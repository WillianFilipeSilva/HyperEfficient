using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Dtos.Equipamento;
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
                   GASTOKWH AS {nameof(Equipamento.PotenciaKwh)},
                   SETORID AS {nameof(Equipamento.SetorId)},
                   CATEGORIAID AS {nameof(Equipamento.CategoriaId)},
                   DESCRICAO AS {nameof(Equipamento.Descricao)},
                   NOME AS {nameof(Equipamento.Nome)},
                   ATIVO AS {nameof(Equipamento.Ativo)},
                   DEVICEIDINTEGRATION AS {nameof(Equipamento.DeviceIdIntegration)}
            FROM EQUIPAMENTO
            LIMIT @pageSize OFFSET @offset";

            return await _connection.ExecuteQueryAsync<Equipamento>(sql, new { pageSize, offset });
        }

        public async Task<IEnumerable<EquipamentoDto>> GetEquipamentoProjection(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var offset = (page - 1) * pageSize;

            var sql = @"
            SELECT
                e.Id         AS Id,
                e.Nome       AS Nome,
                e.Descricao  AS Descricao,
                e.PotenciaKwh   AS PotenciaKwh,
                e.Ativo      AS Ativo,
                e.DeviceIdIntegration AS DeviceIdIntegration,
                c.Id         AS CategoriaId,
                c.Id         AS Id,
                c.Nome       AS Nome,
                s.Id         AS SetorId,
                s.Id         AS Id,
                s.Nome       AS Nome,
                s.Descricao  AS Descricao
            FROM Equipamento e
            JOIN Categoria c ON c.Id = e.CategoriaId
            JOIN Setor     s ON s.Id = e.SetorId
            LIMIT @pageSize OFFSET @offset;";

            return await _connection.ExecuteQueryMapAsync<EquipamentoDto, Categoria, Setor>(sql,
                (eq, cat, set) =>
                {
                    eq.Categoria = cat;
                    eq.Setor = set;
                    return eq;
                },
                new { pageSize, offset },
                "CategoriaId,SetorId");
        }

        public async Task<int> InactiveEquipamento(int id)
        {
            var sql = @"
                UPDATE Equipamento
                SET Ativo = 0
                WHERE Id = @id;";

            return await _connection.ExecuteAsync(sql, new { id });
        }

        public async Task<int> ActiveEquipamento(int id)
        {
            var sql = @"
                UPDATE Equipamento
                SET Ativo = 1
                WHERE Id = @id;";

            return await _connection.ExecuteAsync(sql, new { id });
        }
    }
}