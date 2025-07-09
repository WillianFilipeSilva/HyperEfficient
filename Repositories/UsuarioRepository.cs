using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;
using HyperEfficient.Repositories.Base;

namespace HyperEfficient.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Usuario>> GetPaged(int page, int pageSize)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            var offset = (page - 1) * pageSize;

            var sql = $@"
            SELECT ID AS {nameof(Usuario.Id)},
                   NOME AS {nameof(Usuario.Nome)},
                   CRIADOEM AS {nameof(Usuario.CriadoEm)},
                   SENHA AS {nameof(Usuario.Senha)},
                   EMAIL AS {nameof(Usuario.Email)},
                   ATIVO AS {nameof(Usuario.Ativo)}
            FROM USUARIO
            LIMIT @pageSize OFFSET @offset";

            return await _connection.ExecuteQueryAsync<Usuario>(sql, new { pageSize, offset });
        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            var sql = $@"
            SELECT ID AS {nameof(Usuario.Id)},
                   NOME AS {nameof(Usuario.Nome)},
                   CRIADOEM AS {nameof(Usuario.CriadoEm)},
                   SENHA AS {nameof(Usuario.Senha)},
                   EMAIL AS {nameof(Usuario.Email)},
                   ATIVO AS {nameof(Usuario.Ativo)}
            FROM USUARIO
            WHERE EMAIL = @email";

            return await _connection.ExecuteQueryFirstAsync<Usuario>(sql, new { email });
        }
    }
}