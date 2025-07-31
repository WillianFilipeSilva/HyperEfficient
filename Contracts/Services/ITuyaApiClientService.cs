using HyperEfficient.Dtos.Equipamento;

namespace HyperEfficient.Contracts.Services
{
    public interface ITuyaApiClientService
    {
        public Task<EquipamentoStatusDto> GetStatusAsync(string deviceId);
        public Task LigarAsync(string deviceId);
        public Task DesligarAsync(string deviceId);
    }
}