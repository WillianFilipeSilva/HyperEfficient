using MySqlConnector;

namespace MinhaHyperEfficient.Contracts.Infrastructure
{
    public interface IConnection
    {
        MySqlConnection GetConnection();
        Task<int> ExecuteAsync(string sql, object obj);
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object? param = null);
        Task<T> ExecuteQueryFirstAsync<T>(string sql, object param);
    }
}
