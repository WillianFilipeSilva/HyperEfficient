using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;

namespace HyperEfficient.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IConnection _connection;

        public CategoriaRepository(IConnection connection)
        {
            _connection = connection;
        }

        public async Task Insert(CategoriaEntity categoria)
        {
            string sql = $@"
            INSERT INTO CATEGORIA (NOME)
                VALUE (@Nome)";

            await _connection.ExecuteAsync(sql, categoria);
        }

        public async Task Update(CategoriaEntity categoria)
        {
            string sql = @"
                   UPDATE CATEGORIA
                     SET NOME = @Nome
                     WHERE ID = @Id";

            await _connection.ExecuteAsync(sql, categoria);
        }

        public async Task Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM CATEGORIA WHERE ID = @id", new { id });
        }

        public async Task<IEnumerable<CategoriaEntity>> GetAll()
        {
            string sql = $@"
                SELECT ID AS {nameof(CategoriaEntity.Id)},
                       NOME AS {nameof(CategoriaEntity.Nome)}
                FROM CATEGORIA";

            return await _connection.ExecuteQueryAsync<CategoriaEntity>(sql);
        }

        public async Task<CategoriaEntity> GetById(int id)
        {
            string sql = $@"
                     SELECT ID AS {nameof(CategoriaEntity.Id)},
                            NOME AS {nameof(CategoriaEntity.Nome)}
                     FROM CATEGORIA
                     WHERE ID = @id";

            return await _connection.ExecuteQueryFirstAsync<CategoriaEntity>(sql, new { id });
        }
    }
}
