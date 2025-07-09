using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;
using HyperEfficient.Repositories.Base;

namespace HyperEfficient.Repositories
{
    public class EquipamentoRepository : RepositoryBase<Equipamento>, IEquipamentoRepository
    {
        public EquipamentoRepository(IConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Equipamento>> GetPaged(int page, int pageSize)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            var offset = (page - 1) * pageSize;

            var sql = $@"
            SELECT ID AS {nameof(Equipamento.Id)},
                   GASTOKWH AS {nameof(Equipamento.Gastokwh)},
                   SETORID AS {nameof(Equipamento.SetorId)},
                   CATEGORIAID AS {nameof(Equipamento.CategoriaId)},
                   DESCRICAO AS {nameof(Equipamento.Descricao)},
                   NOME AS {nameof(Equipamento.Nome)},
                   ATIVO AS {nameof(Equipamento.Ativo)}
            FROM EQUIPAMENTO
            LIMIT @pageSize OFFSET @offset";

            return await _connection.ExecuteQueryAsync<Equipamento>(sql, new { pageSize, offset });
        }
    }
}