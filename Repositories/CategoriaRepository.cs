using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Entities;
using HyperEfficient.Repositories.Base;

namespace HyperEfficient.Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IConnection connection) : base(connection)
        {
        }
    }
}