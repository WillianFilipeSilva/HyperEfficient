using HyperEfficient.Contracts.Repositories.Base;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Repositories
{
    public interface IEquipamentoRepository : IRepositoryBase<Equipamento>
    {
        Task<IEnumerable<Equipamento>> GetPaged(int page, int pageSize);
    }
}