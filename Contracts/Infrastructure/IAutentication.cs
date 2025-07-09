using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Infrastructure
{
    public interface IAutentication
    {
        string GenerateToken(Usuario usuarioEntity, TimeSpan tempoExpiracao);
    }
}