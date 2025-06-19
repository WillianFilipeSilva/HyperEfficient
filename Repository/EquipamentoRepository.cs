using HyperEfficient.Contracts.Repository;
using HyperEfficient.Entity;
using MinhaHyperEfficient.Contracts.Infrastructure;

namespace HyperEfficient.Repository
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        private readonly IConnection _connection;

        public EquipamentoRepository(IConnection connection)
        {
            _connection = connection;
        }

        public async Task Insert(EquipamentoEntity equipamento)
        {
            string sql = $@"
            INSERT INTO EQUIPAMENTO (GASTOKWH, SETORID, CATEGORIAID, DESCRICAO, NOME, ATIVO)
                VALUE (
                    @GastokWh,
                    @SetorId,
                    @CategoriaId,
                    @Descricao,
                    @Nome,
                    @Ativo
                )";

            await _connection.ExecuteAsync(sql, equipamento);
        }

        public async Task Update(EquipamentoEntity equipamento)
        {
            string sql = @"
                   UPDATE EQUIPAMENTO
                     SET GASTOKWH = @GastokWh,
                         SETORID = @SetorId,
                         CATEGORIAID = @CategoriaId,
                         DESCRICAO = @Descricao,
                         NOME = @Nome,
                         ATIVO = @Ativo
                     WHERE ID = @Id";

            await _connection.ExecuteAsync(sql, equipamento);
        }

        public async Task Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM EQUIPAMENTO WHERE ID = @id", new { id });
        }

        public async Task<IEnumerable<EquipamentoEntity>> GetAll()
        {
            string sql = $@"
                SELECT ID AS {nameof(EquipamentoEntity.Id)},
                       GASTOKWH AS {nameof(EquipamentoEntity.GastokWh)},
                       SETORID AS {nameof(EquipamentoEntity.SetorId)},
                       CATEGORIAID AS {nameof(EquipamentoEntity.CategoriaId)},
                       DESCRICAO AS {nameof(EquipamentoEntity.Descricao)},
                       NOME AS {nameof(EquipamentoEntity.Nome)},
                       ATIVO AS {nameof(EquipamentoEntity.Ativo)}
                FROM EQUIPAMENTO";

            return await _connection.ExecuteQueryAsync<EquipamentoEntity>(sql);
        }
        public async Task<EquipamentoEntity> GetById(int id)
        {
            string sql = $@"
                     SELECT ID AS {nameof(EquipamentoEntity.Id)},
                            GASTOKWH AS {nameof(EquipamentoEntity.GastokWh)},
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
}
