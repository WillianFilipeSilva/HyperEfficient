using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Dtos.Relatorios;

namespace HyperEfficient.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        protected readonly IConnection _connection;

        public RelatorioRepository(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<TotalizadoresDto> GetTotalizadores(DateTime dataInicio, DateTime dataFim)
        {
            const string sql = @"
                SELECT
                    (SELECT COUNT(*) FROM Setor)       AS QuantidadeSetores,
                    (SELECT COUNT(*) FROM Equipamento) AS QuantidadeEquipamentos,
                    COALESCE(SUM(
                        CASE 
                            WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                                GREATEST(0, TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                            WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                                GREATEST(0, TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                            ELSE 0
                        END
                    )/3600,0) AS TempoUsoTotal,
                    COALESCE(SUM(
                        CASE 
                            WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                                GREATEST(0, (TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                            WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                                GREATEST(0, (TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                            ELSE 0
                        END
                    ),0) AS GastoEnergeticoTotal
                FROM Registro r
                LEFT JOIN Equipamento e ON e.Id = r.EquipamentoId
                WHERE r.DataInicial < @dataFim
                  AND IFNULL(r.DataFinal, NOW()) > @dataInicio;";

            return await _connection.ExecuteQueryFirstAsync<TotalizadoresDto>(sql, new { dataInicio, dataFim })
                   ?? throw new KeyNotFoundException("Não foi possível calcular os totalizadores.");
        }

        public async Task<IEnumerable<DadosMensaisDto>> GetDadosMensais(DateTime dataInicio, DateTime dataFim)
        {
            const string sql = @"
                SELECT DATE_FORMAT(r.DataInicial,'%Y-%m')     AS Mes,
                       UPPER(DATE_FORMAT(r.DataInicial,'%b')) AS MesAbreviado,
                       SUM(CASE 
                           WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                               GREATEST(0, (TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                           WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                               GREATEST(0, (TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                           ELSE 0
                       END) AS ConsumoKwh,
                       SUM(CASE 
                           WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                               GREATEST(0, TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                           WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                               GREATEST(0, TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                           ELSE 0
                       END)/3600 AS TempoUso
                FROM Registro r
                JOIN Equipamento e ON e.Id = r.EquipamentoId
                WHERE r.DataInicial < @dataFim
                  AND IFNULL(r.DataFinal, NOW()) > @dataInicio
                GROUP BY Mes, MesAbreviado
                ORDER BY Mes;";

            return await _connection.ExecuteQueryAsync<DadosMensaisDto>(sql, new { dataInicio, dataFim });
        }

        public async Task<IEnumerable<SetorResumoDto>> GetSetorResumo(DateTime dataInicio, DateTime dataFim)
        {
            const string sql = @"
                SELECT
                    s.Id   AS Id,
                    s.Nome AS Nome,
                    COALESCE(SUM(CASE 
                        WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                            GREATEST(0, (TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                        WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                            GREATEST(0, (TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                        ELSE 0
                    END),0) AS GastoTotal,
                    COALESCE(SUM(CASE 
                        WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                            GREATEST(0, TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                        WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                            GREATEST(0, TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                        ELSE 0
                    END)/3600,0) AS TempoUsoTotal,
                    COUNT(DISTINCT e.Id) AS QuantidadeEquipamentos
                FROM Setor s
                LEFT JOIN Equipamento e ON e.SetorId = s.Id
                LEFT JOIN Registro   r  ON r.EquipamentoId = e.Id
                                          AND r.DataInicial < @dataFim
                                          AND IFNULL(r.DataFinal, NOW()) > @dataInicio
                GROUP BY s.Id, s.Nome
                ORDER BY GastoTotal DESC;";

            return await _connection.ExecuteQueryAsync<SetorResumoDto>(sql, new { dataInicio, dataFim });
        }

        public async Task<IEnumerable<CategoriaResumoDto>> GetCategoriaResumo(DateTime dataInicio, DateTime dataFim)
        {
            const string sql = @"
                SELECT
                    c.Id   AS Id,
                    c.Nome AS Nome,
                    COALESCE(SUM(CASE 
                        WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                            GREATEST(0, (TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                        WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                            GREATEST(0, (TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                        ELSE 0
                    END),0) AS GastoTotal,
                    COALESCE(SUM(CASE 
                        WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                            GREATEST(0, TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                        WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                            GREATEST(0, TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                        ELSE 0
                    END)/3600,0) AS TempoUsoTotal,
                    COUNT(DISTINCT e.Id) AS QuantidadeEquipamentos
                FROM Categoria c
                LEFT JOIN Equipamento e ON e.CategoriaId = c.Id
                LEFT JOIN Registro   r  ON r.EquipamentoId = e.Id
                                          AND r.DataInicial < @dataFim
                                          AND IFNULL(r.DataFinal, NOW()) > @dataInicio
                GROUP BY c.Id, c.Nome
                ORDER BY GastoTotal DESC;";

            return await _connection.ExecuteQueryAsync<CategoriaResumoDto>(sql, new { dataInicio, dataFim });
        }

        public async Task<RelatorioSetorResponse> GetRelatorioSetor(int setorId, DateTime dataInicio, DateTime dataFim)
        {
            const string sql = @"
                SELECT s.Id AS SetorId, s.Nome AS NomeSetor,
                       COUNT(DISTINCT e.Id) AS QuantidadeEquipamentos,
                       COALESCE(SUM(CASE 
                           WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                               GREATEST(0, TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                           WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                               GREATEST(0, TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                           ELSE 0
                       END)/3600,0) AS TempoUsoTotal,
                       COALESCE(SUM(CASE 
                           WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                               GREATEST(0, (TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                           WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                               GREATEST(0, (TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                           ELSE 0
                       END),0) AS GastoEnergeticoTotal
                FROM Setor s
                LEFT JOIN Equipamento e ON e.SetorId = s.Id
                LEFT JOIN Registro   r  ON r.EquipamentoId = e.Id
                                          AND r.DataInicial < @dataFim
                                          AND IFNULL(r.DataFinal, NOW()) > @dataInicio
                WHERE s.Id = @setorId
                GROUP BY s.Id, s.Nome;";

            return await _connection.ExecuteQueryFirstAsync<RelatorioSetorResponse>(sql,
                       new { setorId, dataInicio, dataFim })
                   ?? throw new KeyNotFoundException("Relatório do setor não encontrado!");
        }

        public async Task<RelatorioEquipamentoResponse> GetRelatorioEquipamento(int equipamentoId, DateTime dataInicio,
            DateTime dataFim)
        {
            const string sql = @"
                SELECT e.Id AS EquipamentoId, e.Nome AS NomeEquipamento,
                       COALESCE(SUM(CASE 
                           WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                               GREATEST(0, TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                           WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                               GREATEST(0, TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim)))
                           ELSE 0
                       END)/3600,0) AS TempoUsoTotal,
                       COALESCE(SUM(CASE 
                           WHEN r.DataInicial < @dataInicio AND IFNULL(r.DataFinal, NOW()) > @dataInicio THEN
                               GREATEST(0, (TIMESTAMPDIFF(SECOND, @dataInicio, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                           WHEN r.DataInicial >= @dataInicio AND r.DataInicial < @dataFim THEN
                               GREATEST(0, (TIMESTAMPDIFF(SECOND, r.DataInicial, LEAST(IFNULL(r.DataFinal, NOW()), @dataFim))/3600)*e.Gastokwh)
                           ELSE 0
                       END),0) AS GastoEnergeticoTotal,
                       e.Ativo
                FROM Equipamento e
                LEFT JOIN Registro r ON r.EquipamentoId = e.Id
                                       AND r.DataInicial < @dataFim
                                       AND IFNULL(r.DataFinal, NOW()) > @dataInicio
                WHERE e.Id = @equipamentoId
                GROUP BY e.Id, e.Nome, e.Ativo;";

            return await _connection.ExecuteQueryFirstAsync<RelatorioEquipamentoResponse>(sql,
                       new { equipamentoId, dataInicio, dataFim })
                   ?? throw new KeyNotFoundException("Relatório do equipamento não encontrado!");
        }
    }
}