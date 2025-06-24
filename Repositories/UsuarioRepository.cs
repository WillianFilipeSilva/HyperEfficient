using HyperEfficient.Entity;
using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;

namespace HyperEfficient.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConnection _connection;
        public UsuarioRepository(IConnection connection)
        {
            _connection = connection;
        }

        public async Task Insert(UsuarioEntity usuario)
        {
            string sql = $@"
            INSERT INTO USUARIO (NOME, CRIADOEM, SENHA, EMAIL, ATIVO)
                VALUE (
                    @Nome,
                    @CriadoEm,
                    @Senha,
                    @Email,
                    @Ativo
                )";

            await _connection.ExecuteAsync(sql, usuario);
        }

        public async Task Update(UsuarioEntity usuario)
        {
            string sql = @"
                   UPDATE USUARIO
                     SET NOME = @Nome,
                         CRIADOEM = @CriadoEm,
                         SENHA = @Senha,
                         EMAIL = @Email,
                         ATIVO = @Ativo
                     WHERE ID = @Id";

            await _connection.ExecuteAsync(sql, usuario);
        }

        public async Task Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM USUARIO WHERE ID = @id", new { id });
        }

        public async Task<IEnumerable<UsuarioEntity>> GetAll()
        {
            string sql = $@"
                SELECT ID AS {nameof(UsuarioEntity.Id)},
                       NOME AS {nameof(UsuarioEntity.Nome)},
                       CRIADOEM AS {nameof(UsuarioEntity.CriadoEm)},
                       SENHA AS {nameof(UsuarioEntity.Senha)},
                       EMAIL AS {nameof(UsuarioEntity.Email)},
                       ATIVO AS {nameof(UsuarioEntity.Ativo)}
                FROM USUARIO";

            return await _connection.ExecuteQueryAsync<UsuarioEntity>(sql);
        }

        public async Task<UsuarioEntity?> GetById(int id)
        {
            string sql = $@"
                     SELECT ID AS {nameof(UsuarioEntity.Id)},
                            NOME AS {nameof(UsuarioEntity.Nome)},
                            CRIADOEM AS {nameof(UsuarioEntity.CriadoEm)},
                            SENHA AS {nameof(UsuarioEntity.Senha)},
                            EMAIL AS {nameof(UsuarioEntity.Email)},
                            ATIVO AS {nameof(UsuarioEntity.Ativo)}
                     FROM USUARIO
                     WHERE ID = @id";

            return await _connection.ExecuteQueryFirstAsync<UsuarioEntity>(sql, new { id });
        }

        public async Task<UsuarioEntity?> GetByEmail(string email)
        {
            string sql = $@"
        SELECT ID       AS {nameof(UsuarioEntity.Id)},
               NOME     AS {nameof(UsuarioEntity.Nome)},
               CRIADOEM AS {nameof(UsuarioEntity.CriadoEm)},
               SENHA    AS {nameof(UsuarioEntity.Senha)},
               EMAIL    AS {nameof(UsuarioEntity.Email)},
               ATIVO    AS {nameof(UsuarioEntity.Ativo)}
        FROM USUARIO
        WHERE EMAIL = @email";

            return await _connection.ExecuteQueryFirstAsync<UsuarioEntity>(sql, new { email });
        }
    }
}
