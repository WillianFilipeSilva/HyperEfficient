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

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodos", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Swagger + Bearer no header
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HyperEfficient API",
        Version = "v1",
        Description = "Sistema de gestão de gastos elétricos empresariais"
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

// JWT Authentication
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
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

// Dependency Injection
builder.Services.AddSingleton<IConnection, Connection>();
builder.Services.AddScoped<IAutentication, Autentication>();
builder.Services.AddTransient<IRelatorioService, RelatorioService>();

// Database Initialization
EnsureDatabaseAndTablesCreated(
    builder.Services.BuildServiceProvider().GetRequiredService<IConnection>(),
    builder.Configuration
);

// Extension Methods para DI
builder.Services
    .AddCamadaInfra()
    .AddCamadaAplicacao();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Pipeline de Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HyperEfficient v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors("PermitirTodos");

// Middleware customizado de tratamento de erros
app.UseMiddleware<ErrorHandlingMiddleware>();

// Autenticação e Autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapping dos controllers
app.MapControllers();

app.Run();