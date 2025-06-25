using HyperEfficient.Contracts.Repositories.Base;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Repositories;

public interface IEquipamentoRepository : IRepositoryBase<EquipamentoEntity>
{
    Task<IEnumerable<EquipamentoEntity>> GetPaged(int page, int pageSize);
}