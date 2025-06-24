using HyperEfficient.Entity;

namespace HyperEfficient.Contracts.Infrastructure
{
    public interface IAutentication
    {
        string GenerateToken(UsuarioEntity usuarioEntity);

    }
}
