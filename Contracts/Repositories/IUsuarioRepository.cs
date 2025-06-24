using HyperEfficient.Contracts.Repository.Base;
using HyperEfficient.Entity;

namespace HyperEfficient.Contracts.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<UsuarioEntity>
    {
        Task<UsuarioEntity> GetByEmail(string email);
    }
}
