using HyperEfficient.Dtos.Relatorios;

namespace HyperEfficient.Contracts.Services
{
    public interface IRelatorioService
    {
        Task<RelatorioEmpresaResponse> GetRelatorioEmpresa(DateTime dataInicio, DateTime dataFim);
        Task<RelatorioSetorResponse> GetRelatorioSetor(int setorId, DateTime dataInicio, DateTime dataFim);

        Task<RelatorioEquipamentoResponse> GetRelatorioEquipamento(int equipamentoId, DateTime dataInicio, DateTime dataFim);
    }
}