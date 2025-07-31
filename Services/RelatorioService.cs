using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Relatorios;

namespace HyperEfficient.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService(IConnection connection, IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public async Task<RelatorioEmpresaResponse> GetRelatorioEmpresa(DateTime dataInicio, DateTime dataFim)
        {
            return new RelatorioEmpresaResponse
            {
                Totalizadores = await _relatorioRepository.GetTotalizadores(dataInicio, dataFim),
                DadosMensais = (await _relatorioRepository.GetDadosMensais(dataInicio, dataFim)).ToList(),
                Setores = (await _relatorioRepository.GetSetorResumo(dataInicio, dataFim)).ToList(),
                Categorias = (await _relatorioRepository.GetCategoriaResumo(dataInicio, dataFim)).ToList()
            };
        }

        public async Task<RelatorioSetorResponse> GetRelatorioSetor(int setorId, DateTime dataInicio, DateTime dataFim)
        {
            return await _relatorioRepository.GetRelatorioSetor(setorId, dataInicio, dataFim);
        }

        public async Task<RelatorioEquipamentoResponse> GetRelatorioEquipamento(int equipamentoId, DateTime dataInicio,
            DateTime dataFim
        )
        {
            return await _relatorioRepository.GetRelatorioEquipamento(equipamentoId, dataInicio, dataFim);
        }
    }
}