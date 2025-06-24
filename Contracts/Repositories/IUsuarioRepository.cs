using HyperEfficient.Contracts.Repository.Base;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<UsuarioEntity>
    {
        Task<UsuarioEntity> GetByEmail(string email);
    }
}
