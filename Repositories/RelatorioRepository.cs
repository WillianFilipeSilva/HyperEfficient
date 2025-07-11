using HyperEfficient.Contracts.Infrastructure;

namespace HyperEfficient.Repositories
{
    public class RelatorioRepository
    {
        protected readonly IConnection _connection;

        public RelatorioRepository(IConnection connection)
        {
            _connection = connection;
        }

        //public async Task<List<RegistroDetalhadoDTO>> GetRegistrosComRelacionamentos(DateTime dataInicio, DateTime dataFim)
        //{
        //    var sql = @"
        //        SELECT
        //            r.Id AS RegistroId,
        //            r.DataInicial,
        //            r.DataFinal,
        //            e.Id AS EquipamentoId,
        //            e.Nome AS EquipamentoNome,
        //            e.Gastokwh,
        //            c.Id AS CategoriaId,
        //            c.Nome AS CategoriaNome,
        //            s.Id AS SetorId,
        //            s.Nome AS SetorNome
        //        FROM Registro r
        //        JOIN Equipamento e ON e.Id = r.EquipamentoId
        //        JOIN Categoria c ON c.Id = e.CategoriaId
        //        JOIN Setor s ON s.Id = e.SetorId
        //        WHERE r.DataInicial >= @dataInicio AND IFNULL(r.DataFinal, NOW()) <= @dataFim
        //    ";

        //    var result = await _connection.ExecuteQueryAsync<RegistroDetalhadoDTO>(sql, new { dataInicio, dataFim });
        //    return result.ToList();
        //}
    }
}