using HyperEfficient.Infrastructure;
using HyperEfficient.Infrastructure.Extensions;
using HyperEfficient.Infrastructure.Middleware;
using HyperEfficient.Contracts.Service;
using HyperEfficient.Services;
using Microsoft.OpenApi.Models;
using HyperEfficient.Contracts.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HyperEfficient API",
        Version = "v1"
    });
});

// Connection (lê a ConnectionString em appsettings.json)
builder.Services.AddSingleton<IConnection, Connection>();

// Registro automático de Repositories e Services
builder.Services
    .AddCamadaInfra()
    .AddCamadaAplicacao();

// Garantia explícita caso o scanner não encontre
builder.Services.AddTransient<IRelatorioService, RelatorioService>();

// AutoMapper – carrega todos os Profiles de todos os assemblies carregados
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HyperEfficient v1"));
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();   // captura exceções e manda uma mensagem Genérica :)
app.UseAuthorization();

app.MapControllers();
app.Run();
