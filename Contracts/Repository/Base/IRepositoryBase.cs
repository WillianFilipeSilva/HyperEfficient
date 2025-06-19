namespace HyperEfficient.Contracts.Repository.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
    }
}
