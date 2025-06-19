using Dapper;
using MinhaHyperEfficient.Contracts.Infrastructure;
using MySqlConnector;

namespace HyperEfficient.Infrastructure
{
    public class Connection : IConnection
    {
        private readonly string _conn;

        public Connection(IConfiguration cfg)
        {
            _conn = cfg.GetConnectionString("Default")
                   ?? throw new ArgumentException("Connection string 'Default' não encontrada");
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_conn);
        }

        public async Task<int> ExecuteAsync(string sql, object param)
        {
            using var con = GetConnection();
            return await con.ExecuteAsync(sql, param);
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object? param = null)
        {
            using var con = GetConnection();
            return param is null
                ? await con.QueryAsync<T>(sql)
                : await con.QueryAsync<T>(sql, param);
        }

        public async Task<T> ExecuteQueryFirstAsync<T>(string sql, object param)
        {
            using var con = GetConnection();
            return await con.QueryFirstAsync<T>(sql, param);
        }
    }
}
