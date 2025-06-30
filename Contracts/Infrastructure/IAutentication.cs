using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Infrastructure;

public interface IAutentication
{
    string GenerateToken(UsuarioEntity usuarioEntity, TimeSpan tempoExpiracao);
}