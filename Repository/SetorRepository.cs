using MinhaHyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repository;
using HyperEfficient.Entity;

namespace HyperEfficient.Repository
{
    public class SetorRepository : ISetorRepository
    {
        private readonly IConnection _connection;

        public SetorRepository(IConnection connection)
        {
            _connection = connection;
        }

        public async Task Insert(SetorEntity setor)
        {
            string sql = $@"
            INSERT INTO SETOR (GASTOGERAL, NOMESETOR)
                VALUE (
                    @GastoGeral,
                    @NomeSetor
                )";

            await _connection.ExecuteAsync(sql, setor);
        }

        public async Task Update(SetorEntity setor)
        {
            string sql = @"
                   UPDATE SETOR
                     SET GASTOGERAL = @GastoGeral,
                         NOMESETOR = @NomeSetor
                     WHERE ID = @Id";

            await _connection.ExecuteAsync(sql, setor);
        }

        public async Task Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM SETOR WHERE ID = @id", new { id });
        }

        public async Task<IEnumerable<SetorEntity>> GetAll()
        {
            string sql = $@"
                SELECT ID AS {nameof(SetorEntity.Id)},
                       GASTOGERAL AS {nameof(SetorEntity.GastoGeral)},
                       NOMESETOR AS {nameof(SetorEntity.NomeSetor)}
                FROM SETOR";

            return await _connection.ExecuteQueryAsync<SetorEntity>(sql);
        }

        public async Task<SetorEntity> GetById(int id)
        {
            string sql = $@"
                     SELECT ID AS {nameof(SetorEntity.Id)},
                            GASTOGERAL AS {nameof(SetorEntity.GastoGeral)},
                            NOMESETOR AS {nameof(SetorEntity.NomeSetor)}
                     FROM SETOR
                     WHERE ID = @id";

            return await _connection.ExecuteQueryFirstAsync<SetorEntity>(sql, new { id });
        }
    }
}
