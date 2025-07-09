using HyperEfficient.Contracts.Repositories.Base;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Repositories
{
    public interface ISetorRepository : IRepositoryBase<Setor>
    {
        Task<IEnumerable<Setor>> GetPaged(int page, int pageSize);
    }
}