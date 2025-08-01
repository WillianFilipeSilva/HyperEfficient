using HyperEfficient.Contracts.Services;

namespace HyperEfficient.Services.BackgroundServices
{
    public class TuyaUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TuyaUpdateService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public TuyaUpdateService(
            IServiceProvider serviceProvider,
            ILogger<TuyaUpdateService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Serviço de atualização na integração Tuya iniciado às {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await AtualizarEquipamentosTuya();
                    _logger.LogInformation("Atualização de equipamentos na integração Tuya concluída às {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar equipamentos na integração Tuya: {Message}", ex.Message);
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task AtualizarEquipamentosTuya()
        {
            using var scope = _serviceProvider.CreateScope();
            var equipamentoService = scope.ServiceProvider.GetRequiredService<IEquipamentoService>();
            await equipamentoService.AtualizarConsumoEquipamentos();
        }
    }
}