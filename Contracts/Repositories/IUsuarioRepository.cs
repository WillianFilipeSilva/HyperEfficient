using HyperEfficient.Contracts.Repositories.Base;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Repositories;

public interface IUsuarioRepository : IRepositoryBase<UsuarioEntity>
{
    Task<UsuarioEntity?> GetByEmail(string email);
    Task<IEnumerable<UsuarioEntity>> GetPaged(int page, int pageSize);
}