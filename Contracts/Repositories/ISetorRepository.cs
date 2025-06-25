using HyperEfficient.Contracts.Repositories.Base;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Repositories;

public interface ISetorRepository : IRepositoryBase<SetorEntity>
{
    Task<IEnumerable<SetorEntity>> GetPaged(int page, int pageSize);
}