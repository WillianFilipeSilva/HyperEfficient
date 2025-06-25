using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;

namespace HyperEfficient.Repositories;

public class EquipamentoRepository : IEquipamentoRepository
{
    private readonly IConnection _connection;

    public EquipamentoRepository(IConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> Insert(EquipamentoEntity equipamento)
    {
        var sql = @"
            INSERT INTO EQUIPAMENTO (GASTOKWH, SETORID, CATEGORIAID, DESCRICAO, NOME, ATIVO)
                VALUES (
                    @Gastokwh,
                    @SetorId,
                    @CategoriaId,
                    @Descricao,
                    @Nome,
                    @Ativo
                );";

        return await _connection.ExecuteAsync(sql, equipamento);
    }

    public async Task<int> Update(EquipamentoEntity equipamento)
    {
        var sql = @"
            UPDATE EQUIPAMENTO
                SET GASTOKWH = @Gastokwh,
                    SETORID = @SetorId,
                    CATEGORIAID = @CategoriaId,
                    DESCRICAO = @Descricao,
                    NOME = @Nome,
                    ATIVO = @Ativo
            WHERE ID = @Id";

        return await _connection.ExecuteAsync(sql, equipamento);
    }

    public async Task<int> Delete(int id)
    {
        return await _connection.ExecuteAsync("DELETE FROM EQUIPAMENTO WHERE ID = @id", new { id });
    }

    public async Task<IEnumerable<EquipamentoEntity>> GetAll()
    {
        var sql = $@"
            SELECT ID AS {nameof(EquipamentoEntity.Id)},
                   GASTOKWH AS {nameof(EquipamentoEntity.Gastokwh)},
                   SETORID AS {nameof(EquipamentoEntity.SetorId)},
                   CATEGORIAID AS {nameof(EquipamentoEntity.CategoriaId)},
                   DESCRICAO AS {nameof(EquipamentoEntity.Descricao)},
                   NOME AS {nameof(EquipamentoEntity.Nome)},
                   ATIVO AS {nameof(EquipamentoEntity.Ativo)}
            FROM EQUIPAMENTO";

        return await _connection.ExecuteQueryAsync<EquipamentoEntity>(sql);
    }

    public async Task<IEnumerable<EquipamentoEntity>> GetPaged(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        var offset = (page - 1) * pageSize;

        var sql = $@"
            SELECT ID AS {nameof(EquipamentoEntity.Id)},
                   GASTOKWH AS {nameof(EquipamentoEntity.Gastokwh)},
                   SETORID AS {nameof(EquipamentoEntity.SetorId)},
                   CATEGORIAID AS {nameof(EquipamentoEntity.CategoriaId)},
                   DESCRICAO AS {nameof(EquipamentoEntity.Descricao)},
                   NOME AS {nameof(EquipamentoEntity.Nome)},
                   ATIVO AS {nameof(EquipamentoEntity.Ativo)}
            FROM EQUIPAMENTO
            LIMIT @pageSize OFFSET @offset";

        return await _connection.ExecuteQueryAsync<EquipamentoEntity>(sql, new { pageSize, offset });
    }

    public async Task<EquipamentoEntity?> GetById(int id)
    {
        var sql = $@"
            SELECT ID AS {nameof(EquipamentoEntity.Id)},
                   GASTOKWH AS {nameof(EquipamentoEntity.Gastokwh)},
                   SETORID AS {nameof(EquipamentoEntity.SetorId)},
                   CATEGORIAID AS {nameof(EquipamentoEntity.CategoriaId)},
                   DESCRICAO AS {nameof(EquipamentoEntity.Descricao)},
                   NOME AS {nameof(EquipamentoEntity.Nome)},
                   ATIVO AS {nameof(EquipamentoEntity.Ativo)}
            FROM EQUIPAMENTO
            WHERE ID = @id";

        return await _connection.ExecuteQueryFirstAsync<EquipamentoEntity>(sql, new { id });
    }
}
