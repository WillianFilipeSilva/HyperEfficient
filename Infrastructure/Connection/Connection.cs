using Dapper;
using HyperEfficient.Contracts.Infrastructure;
using MySqlConnector;

namespace HyperEfficient.Infrastructure.Connection
{
    public class Connection : IConnection
    {
        private readonly string _conn;

        public Connection(IConfiguration cfg)
        {
            _conn = cfg.GetConnectionString("Default") ??
                    throw new ArgumentException(
                        "Não foi encontrada a string de conexao com o banco no seu AppSettings!");
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

            return param is null ? await con.QueryAsync<T>(sql) : await con.QueryAsync<T>(sql, param);
        }

        public async Task<T?> ExecuteQueryFirstAsync<T>(string sql, object param)
        {
            using var con = GetConnection();

            return await con.QueryFirstOrDefaultAsync<T>(sql, param);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param)
        {
            using var con = GetConnection();

            return await con.ExecuteScalarAsync<T>(sql, param);
        }

        public async Task<IEnumerable<TParent>> ExecuteQueryMapAsync<TParent, TChild>(string sql,
            Func<TParent, TChild, TParent> map,
            object? param = null,
            string splitOn = "Id"
        )
        {
            using var con = GetConnection();
            return await con.QueryAsync<TParent, TChild, TParent>(sql, map, param, splitOn: splitOn);
        }

        public async Task<TParent?> ExecuteQueryMapFirstAsync<TParent, TChild>(string sql,
            Func<TParent, TChild, TParent> map,
            object param,
            string splitOn = "Id"
        )
        {
            using var con = GetConnection();
            return (await con.QueryAsync<TParent, TChild, TParent>(sql, map, param, splitOn: splitOn))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<TParent>> ExecuteQueryMapAsync<TParent, TChild1, TChild2>(string sql,
            Func<TParent, TChild1, TChild2, TParent> map,
            object? param = null,
            string splitOn = "Id1,Id2"
        )
        {
            using var con = GetConnection();
            return await con.QueryAsync<TParent, TChild1, TChild2, TParent>(sql, map, param, splitOn: splitOn);
        }
    }
}