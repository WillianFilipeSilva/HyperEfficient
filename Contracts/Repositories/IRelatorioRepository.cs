using HyperEfficient.Dtos.Relatorios;

namespace HyperEfficient.Contracts.Repositories
{
    public interface IRelatorioRepository
    {
        Task<TotalizadoresDto> GetTotalizadores(DateTime dataInicio, DateTime dataFim);

        Task<IEnumerable<DadosMensaisDto>> GetDadosMensais(DateTime dataInicio, DateTime dataFim);

        Task<IEnumerable<SetorResumoDto>> GetSetorResumo(DateTime dataInicio, DateTime dataFim);

        Task<IEnumerable<CategoriaResumoDto>> GetCategoriaResumo(DateTime dataInicio, DateTime dataFim);

        Task<RelatorioSetorResponse> GetRelatorioSetor(int setorId, DateTime dataInicio, DateTime dataFim);

        Task<RelatorioEquipamentoResponse> GetRelatorioEquipamento(int equipamentoId, DateTime dataInicio,
            DateTime dataFim
        );
    }
}