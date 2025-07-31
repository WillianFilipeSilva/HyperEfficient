using AutoMapper;
using HyperEfficient.Contracts.Repositories.Base;

namespace HyperEfficient.Services.Base
{
    public abstract class ServiceBase<T> where T : class
    {
        protected readonly IMapper _map;
        protected readonly IRepositoryBase<T> _repository;

        protected ServiceBase(IRepositoryBase<T> repository, IMapper map)
        {
            _repository = repository;
            _map = map;
        }
    }
}