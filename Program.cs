using HyperEfficient.Infrastructure;
using HyperEfficient.Infrastructure.Extensions;
using HyperEfficient.Infrastructure.Mapping;
using MinhaHyperEfficient.Contracts.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnection, Connection>();

builder.Services
    .AddCamadaInfra()
    .AddCamadaAplicacao();

builder.Services.AddAutoMapper(
    typeof(Program).Assembly,
    typeof(CategoriaProfile).Assembly,
    typeof(EquipamentoProfile).Assembly,
    typeof(RegistroProfile).Assembly,
    typeof(UsuarioProfile).Assembly
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
