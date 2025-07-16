using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;
using HyperEfficient.Repositories.Base;

namespace HyperEfficient.Repositories
{
    public class RegistroRepository : RepositoryBase<Registro>, IRegistroRepository
    {
        public RegistroRepository(IConnection connection) : base(connection)
        {
        }

        public async Task<Registro?> GetRegistroByEquipamentoId(int equipamentoId)
        {
            var sql = $@"
            SELECT ID AS {nameof(Registro.Id)},
                   DATAINICIAL AS {nameof(Registro.DataInicial)},
                   DATAFINAL AS {nameof(Registro.DataFinal)},
                   EQUIPAMENTOID AS {nameof(Registro.EquipamentoId)},
                   TOTALTEMPO AS {nameof(Registro.TotalTempo)}
            FROM REGISTRO
            WHERE EQUIPAMENTOID = @equipamentoId
            ORDER BY ID DESC";
            return await _connection.ExecuteQueryFirstAsync<Registro>(sql, new { equipamentoId });
        }

        public async Task<IEnumerable<Registro>> GetPaged(int page, int pageSize)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            var offset = (page - 1) * pageSize;

            var sql = $@"
            SELECT ID AS {nameof(Registro.Id)},
                   DATAINICIAL AS {nameof(Registro.DataInicial)},
                   DATAFINAL AS {nameof(Registro.DataFinal)},
                   EQUIPAMENTOID AS {nameof(Registro.EquipamentoId)},
                   TOTALTEMPO AS {nameof(Registro.TotalTempo)}
            FROM REGISTRO
            LIMIT @pageSize OFFSET @offset";

            return await _connection.ExecuteQueryAsync<Registro>(sql, new { pageSize, offset });
        }
    }
}