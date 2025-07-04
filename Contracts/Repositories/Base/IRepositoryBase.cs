namespace HyperEfficient.Contracts.Repositories.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
    }
}