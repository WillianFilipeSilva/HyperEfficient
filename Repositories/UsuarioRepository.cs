using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;

namespace HyperEfficient.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IConnection _connection;

    public UsuarioRepository(IConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> Insert(UsuarioEntity usuario)
    {
        var sql = @"
            INSERT INTO USUARIO (NOME, CRIADOEM, SENHA, EMAIL, ATIVO)
                VALUES (
                    @Nome,
                    @CriadoEm,
                    @Senha,
                    @Email,
                    @Ativo
                );";

        return await _connection.ExecuteAsync(sql, usuario);
    }

    public async Task<int> Update(UsuarioEntity usuario)
    {
        var sql = @"
            UPDATE USUARIO
                SET NOME = @Nome,
                    CRIADOEM = @CriadoEm,
                    SENHA = @Senha,
                    EMAIL = @Email,
                    ATIVO = @Ativo
            WHERE ID = @Id";

        return await _connection.ExecuteAsync(sql, usuario);
    }

    public async Task<int> Delete(int id)
    {
        return await _connection.ExecuteAsync("DELETE FROM USUARIO WHERE ID = @id", new { id });
    }

    public async Task<IEnumerable<UsuarioEntity>> GetAll()
    {
        var sql = $@"
            SELECT ID AS {nameof(UsuarioEntity.Id)},
                   NOME AS {nameof(UsuarioEntity.Nome)},
                   CRIADOEM AS {nameof(UsuarioEntity.CriadoEm)},
                   SENHA AS {nameof(UsuarioEntity.Senha)},
                   EMAIL AS {nameof(UsuarioEntity.Email)},
                   ATIVO AS {nameof(UsuarioEntity.Ativo)}
            FROM USUARIO";

        return await _connection.ExecuteQueryAsync<UsuarioEntity>(sql);
    }

    public async Task<IEnumerable<UsuarioEntity>> GetPaged(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        var offset = (page - 1) * pageSize;

        var sql = $@"
            SELECT ID AS {nameof(UsuarioEntity.Id)},
                   NOME AS {nameof(UsuarioEntity.Nome)},
                   CRIADOEM AS {nameof(UsuarioEntity.CriadoEm)},
                   SENHA AS {nameof(UsuarioEntity.Senha)},
                   EMAIL AS {nameof(UsuarioEntity.Email)},
                   ATIVO AS {nameof(UsuarioEntity.Ativo)}
            FROM USUARIO
            LIMIT @pageSize OFFSET @offset";

        return await _connection.ExecuteQueryAsync<UsuarioEntity>(sql, new { pageSize, offset });
    }

    public async Task<UsuarioEntity?> GetById(int id)
    {
        var sql = $@"
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
        var sql = $@"
            SELECT ID AS {nameof(UsuarioEntity.Id)},
                   NOME AS {nameof(UsuarioEntity.Nome)},
                   CRIADOEM AS {nameof(UsuarioEntity.CriadoEm)},
                   SENHA AS {nameof(UsuarioEntity.Senha)},
                   EMAIL AS {nameof(UsuarioEntity.Email)},
                   ATIVO AS {nameof(UsuarioEntity.Ativo)}
            FROM USUARIO
            WHERE EMAIL = @email";

        return await _connection.ExecuteQueryFirstAsync<UsuarioEntity>(sql, new { email });
    }
}
