using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;

namespace HyperEfficient.Repositories;

public class SetorRepository : ISetorRepository
{
    private readonly IConnection _connection;

    public SetorRepository(IConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> Insert(SetorEntity setor)
    {
        var sql = @"
            INSERT INTO SETOR (GASTOGERAL, NOME)
                VALUES (
                    @GastoGeral,
                    @Nome,
                    @Descricao
                );";

        return await _connection.ExecuteAsync(sql, setor);
    }

    public async Task<int> Update(SetorEntity setor)
    {
        var sql = @"
            UPDATE SETOR
                SET GASTOGERAL = @GastoGeral,
                    NOME = @Nome
                    DESCRICAO = @Descricao
            WHERE ID = @Id";

        return await _connection.ExecuteAsync(sql, setor);
    }

    public async Task<int> Delete(int id)
    {
        return await _connection.ExecuteAsync("DELETE FROM SETOR WHERE ID = @id", new { id });
    }

    public async Task<IEnumerable<SetorEntity>> GetAll()
    {
        var sql = $@"
            SELECT ID AS {nameof(SetorEntity.Id)},
                   GASTOGERAL AS {nameof(SetorEntity.GastoGeral)},
                   NOME AS {nameof(SetorEntity.Nome)}
                   DESCRICAO AS {nameof(SetorEntity.Descricao)}
            FROM SETOR";

        return await _connection.ExecuteQueryAsync<SetorEntity>(sql);
    }

    public async Task<IEnumerable<SetorEntity>> GetPaged(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        var offset = (page - 1) * pageSize;

        var sql = $@"
            SELECT ID AS {nameof(SetorEntity.Id)},
                   GASTOGERAL AS {nameof(SetorEntity.GastoGeral)},
                   NOME AS {nameof(SetorEntity.Nome)}
                   DESCRICAO AS {nameof(SetorEntity.Descricao)}
            FROM SETOR
            LIMIT @pageSize OFFSET @offset";

        return await _connection.ExecuteQueryAsync<SetorEntity>(sql, new { pageSize, offset });
    }

    public async Task<SetorEntity?> GetById(int id)
    {
        var sql = $@"
            SELECT ID AS {nameof(SetorEntity.Id)},
                   GASTOGERAL AS {nameof(SetorEntity.GastoGeral)},
                   NOME AS {nameof(SetorEntity.Nome)}
                   DESCRICAO AS {nameof(SetorEntity.Descricao)}
            FROM SETOR
            WHERE ID = @id";

        return await _connection.ExecuteQueryFirstAsync<SetorEntity>(sql, new { id });
    }
}
