using MySqlConnector;

namespace HyperEfficient.Contracts.Infrastructure
{
    public interface IConnection
    {
        MySqlConnection GetConnection();
        Task<int> ExecuteAsync(string sql, object obj);
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object? param = null);
        Task<T?> ExecuteQueryFirstAsync<T>(string sql, object param);
        Task<T> ExecuteScalarAsync<T>(string sql, object param);

        Task<IEnumerable<TParent>> ExecuteQueryMapAsync<TParent, TChild>(string sql,
            Func<TParent, TChild, TParent> map,
            object? param = null,
            string splitOn = "Id"
        );

        Task<TParent?> ExecuteQueryMapFirstAsync<TParent, TChild>(string sql,
            Func<TParent, TChild, TParent> map,
            object param,
            string splitOn = "Id"
        );

        Task<IEnumerable<TParent>> ExecuteQueryMapAsync<TParent, TChild1, TChild2>(string sql,
            Func<TParent, TChild1, TChild2, TParent> map,
            object? param = null,
            string splitOn = "Id1,Id2"
        );
    }
}