using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Service;
using HyperEfficient.Infrastructure.Autentication;
using HyperEfficient.Infrastructure.Connection;
using HyperEfficient.Infrastructure.Extensions;
using HyperEfficient.Infrastructure.Middleware;
using HyperEfficient.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static HyperEfficient.Infrastructure.DatabaseInitializer.DatabaseInitializer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger + Bearer no header (apenas uma chamada)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HyperEfficient API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. Ex: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

// JWT config
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Connection (lê a ConnectionString em appsettings.json)
builder.Services.AddSingleton<IConnection, Connection>();

// Cria banco/tabelas se necessário
EnsureDatabaseAndTablesCreated(
    builder.Services.BuildServiceProvider().GetRequiredService<IConnection>(),
    builder.Configuration
);

builder.Services.AddScoped<IAutentication, Autentication>();

// Registro automático de Repositories e Services
builder.Services
    .AddCamadaInfra()
    .AddCamadaAplicacao();

builder.Services.AddTransient<IRelatorioService, RelatorioService>();

// AutoMapper – carrega todos os Profiles de todos os assemblies carregados
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HyperEfficient v1"));
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication(); // ATENÇÃO: UseAuthentication ANTES do Authorization!
app.UseAuthorization();

app.MapControllers();
app.Run();