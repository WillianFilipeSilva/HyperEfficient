using HyperEfficient.Contracts.Service;
using HyperEfficient.DTOs.Relatorios;
using HyperEfficient.Contracts.Infrastructure;

namespace HyperEfficient.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IConnection _connection;

        public RelatorioService(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<RelatorioEmpresaResponse> GetRelatorioEmpresa(DateTime dataInicio, DateTime dataFim)
        {
            const string sql = @"SELECT
                   COUNT(DISTINCT s.Id) QuantidadeSetores,
                   COUNT(DISTINCT e.Id) QuantidadeEquipamentos,
                   SUM(TIMESTAMPDIFF(SECOND, r.DataInicial, IFNULL(r.DataFinal,NOW())))/3600 TempoUsoTotal,
                   SUM((TIMESTAMPDIFF(SECOND, r.DataInicial, IFNULL(r.DataFinal,NOW())))/3600 * e.Gastokwh) GastoEnergeticoTotal
               FROM Registro r
               JOIN Equipamento e ON e.Id = r.EquipamentoId
               JOIN Setor s ON s.Id = e.SetorId
               WHERE r.DataInicial >= @dataInicio AND IFNULL(r.DataFinal,NOW()) <= @dataFim";

            return await _connection.ExecuteQueryFirstAsync<RelatorioEmpresaResponse>(sql, new { dataInicio, dataFim });
        }

        public async Task<RelatorioSetorResponse> GetRelatorioSetor(int setorId, DateTime dataInicio, DateTime dataFim)
        {
            const string sql = @"SELECT
                   s.Id SetorId,
                   s.NomeSetor,
                   COUNT(DISTINCT e.Id) QuantidadeEquipamentos,
                   SUM(TIMESTAMPDIFF(SECOND, r.DataInicial, IFNULL(r.DataFinal,NOW())))/3600 TempoUsoTotal,
                   SUM((TIMESTAMPDIFF(SECOND, r.DataInicial, IFNULL(r.DataFinal,NOW())))/3600 * e.Gastokwh) GastoEnergeticoTotal
                FROM Setor s
                JOIN Equipamento e ON e.SetorId = s.Id
                JOIN Registro r ON r.EquipamentoId = e.Id
                WHERE s.Id = @setorId AND r.DataInicial >= @dataInicio AND IFNULL(r.DataFinal,NOW()) <= @dataFim
                GROUP BY s.Id, s.NomeSetor";

            return await _connection.ExecuteQueryFirstAsync<RelatorioSetorResponse>(sql, new { setorId, dataInicio, dataFim });
        }

        public async Task<RelatorioEquipamentoResponse> GetRelatorioEquipamento(int equipamentoId, DateTime dataInicio, DateTime dataFim)
        {
            const string sql = @"SELECT
                   e.Id EquipamentoId,
                   e.Nome NomeEquipamento,
                   SUM(TIMESTAMPDIFF(SECOND, r.DataInicial, IFNULL(r.DataFinal,NOW())))/3600 TempoUsoTotal,
                   SUM((TIMESTAMPDIFF(SECOND, r.DataInicial, IFNULL(r.DataFinal,NOW())))/3600 * e.Gastokwh) GastoEnergeticoTotal,
                   e.Ativo
                FROM Equipamento e
                JOIN Registro r ON r.EquipamentoId = e.Id
                WHERE e.Id = @equipamentoId AND r.DataInicial >= @dataInicio AND IFNULL(r.DataFinal,NOW()) <= @dataFim
                GROUP BY e.Id, e.Nome, e.Ativo";

            return await _connection.ExecuteQueryFirstAsync<RelatorioEquipamentoResponse>(sql, new { equipamentoId, dataInicio, dataFim });
        }
    }
}
