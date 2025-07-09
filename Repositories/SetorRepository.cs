using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;
using HyperEfficient.Repositories.Base;

namespace HyperEfficient.Repositories
{
    public class SetorRepository : RepositoryBase<Setor>, ISetorRepository
    {
        public SetorRepository(IConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Setor>> GetPaged(int page, int pageSize)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            var offset = (page - 1) * pageSize;

            var sql = $@"
            SELECT ID AS {nameof(Setor.Id)},
                   NOME AS {nameof(Setor.Nome)},
                   DESCRICAO AS {nameof(Setor.Descricao)}
            FROM SETOR
            LIMIT @pageSize OFFSET @offset";

            return await _connection.ExecuteQueryAsync<Setor>(sql, new { pageSize, offset });
        }
    }
}