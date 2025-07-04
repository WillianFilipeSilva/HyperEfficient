using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;

namespace HyperEfficient.Repositories
{
    public class RegistroRepository : IRegistroRepository
    {
        private readonly IConnection _connection;

        public RegistroRepository(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Insert(RegistroEntity registro)
        {
            string sql = @"
            INSERT INTO REGISTRO (DATAINICIAL, DATAFINAL, EQUIPAMENTOID)
                VALUES (
                    @DataInicial,
                    @DataFinal,
                    @EquipamentoId
                );";

            return await _connection.ExecuteAsync(sql, registro);
        }

        public async Task<int> Update(RegistroEntity registro)
        {
            string sql = @"
            UPDATE REGISTRO
                SET DATAINICIAL = @DataInicial,
                    DATAFINAL = @DataFinal,
                    EQUIPAMENTOID = @EquipamentoId
            WHERE ID = @Id";

            return await _connection.ExecuteAsync(sql, registro);
        }

        public async Task<int> Delete(int id)
        {
            return await _connection.ExecuteAsync("DELETE FROM REGISTRO WHERE ID = @id", new { id });
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

        public async Task<IEnumerable<RegistroEntity>> GetPaged(int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int offset = (page - 1) * pageSize;

            string sql = $@"
            SELECT ID AS {nameof(RegistroEntity.Id)},
                   DATAINICIAL AS {nameof(RegistroEntity.DataInicial)},
                   DATAFINAL AS {nameof(RegistroEntity.DataFinal)},
                   EQUIPAMENTOID AS {nameof(RegistroEntity.EquipamentoId)}
            FROM REGISTRO
            LIMIT @pageSize OFFSET @offset";

            return await _connection.ExecuteQueryAsync<RegistroEntity>(sql, new { pageSize, offset });
        }

        public async Task<RegistroEntity?> GetById(int id)
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