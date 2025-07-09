using HyperEfficient.Contracts.Repositories.Base;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario?> GetByEmail(string email);
        Task<IEnumerable<Usuario>> GetPaged(int page, int pageSize);
    }
}