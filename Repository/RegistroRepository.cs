using HyperEfficient.Contracts.Repository;
using HyperEfficient.Entity;
using MinhaHyperEfficient.Contracts.Infrastructure;

namespace HyperEfficient.Repository
{
    public class RegistroRepository : IRegistroRepository
    {
        private readonly IConnection _connection;

        public RegistroRepository(IConnection connection)
        {
            _connection = connection;
        }

        public async Task Insert(RegistroEntity registro)
        {
            string sql = $@"
            INSERT INTO REGISTRO (DATAINICIAL, DATAFINAL, EQUIPAMENTOID)
                VALUE (
                    @DataInicial,
                    @DataFinal,
                    @EquipamentoId
                )";

            await _connection.ExecuteAsync(sql, registro);
        }
        public async Task Update(RegistroEntity registro)
        {
            string sql = @"
                   UPDATE REGISTRO
                     SET DATAINICIAL = @DataInicial,
                         DATAFINAL = @DataFinal,
                         EQUIPAMENTOID = @EquipamentoId
                     WHERE ID = @Id";

            await _connection.ExecuteAsync(sql, registro);
        }

        public async Task Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM REGISTRO WHERE ID = @id", new { id });
        }

        public async Task<IEnumerable<RegistroEntity>> GetAll()
        {
            string sql = $@"
                SELECT ID AS {nameof(RegistroEntity.Id)},
                       DATAINICIAL AS {nameof(RegistroEntity.DataInicial)},
                       DATAFINAL AS {nameof(RegistroEntity.DataFinal)},
                       EQUIPAMENTOID AS {nameof(RegistroEntity.EquipamentoId)}
                FROM REGISTRO";

            return await _connection.ExecuteQueryAsync<RegistroEntity>(sql);
        }

        public async Task<RegistroEntity> GetById(int id)
        {
            string sql = $@"
                     SELECT ID AS {nameof(RegistroEntity.Id)},
                            DATAINICIAL AS {nameof(RegistroEntity.DataInicial)},
                            DATAFINAL AS {nameof(RegistroEntity.DataFinal)},
                            EQUIPAMENTOID AS {nameof(RegistroEntity.EquipamentoId)}
                     FROM REGISTRO
                     WHERE ID = @id";

            return await _connection.ExecuteQueryFirstAsync<RegistroEntity>(sql, new { id });
        }
    }
}
