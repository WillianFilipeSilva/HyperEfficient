using HyperEfficient.Contracts.Repositories.Base;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Repositories
{
    public interface IRegistroRepository : IRepositoryBase<Registro>
    {
        Task<Registro?> GetRegistroByEquipamentoId(int equipamentoId);
        Task<IEnumerable<Registro>> GetPaged(int page, int pageSize);
    }
}