using HyperEfficient.Contracts.Repositories.Base;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Repositories
{
    public interface IRegistroRepository : IRepositoryBase<RegistroEntity>
    {
        Task<IEnumerable<RegistroEntity>> GetPaged(int page, int pageSize);
    }
}