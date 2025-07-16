using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories.Base;
using System.Reflection;

namespace HyperEfficient.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly IEnumerable<string> _columns;
        protected readonly IConnection _connection;
        private readonly string _keyName;
        private readonly string _tableName;

        protected RepositoryBase(IConnection connection)
        {
            _connection = connection;
            var type = typeof(T);

            _tableName = type.Name.ToUpperInvariant();

            var keyProp = type.GetProperty("Id", BindingFlags.Public | BindingFlags.Instance)
                          ?? throw new InvalidOperationException(
                              $"Entity {type.Name} precisa de uma propriedade 'Id'.");

            _keyName = keyProp.Name;

            _columns = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p =>
                    !string.Equals(p.Name, _keyName, StringComparison.OrdinalIgnoreCase) &&
                    p.SetMethod != null && p.SetMethod.IsPublic)
                .Select(p => p.Name);
        }

        public async Task<int> Insert(T entity)
        {
            var cols = string.Join(", ", _columns);
            var vals = string.Join(", ", _columns.Select(c => "@" + c));
            var sql = $"INSERT INTO {_tableName} ({cols}) VALUES ({vals});";
            return await _connection.ExecuteAsync(sql, entity);
        }

        public async Task<int> Update(T entity)
        {
            var setClause = string.Join(", ", _columns.Select(c => $"{c} = @{c}"));
            var sql = $"UPDATE {_tableName} SET {setClause} WHERE {_keyName} = @{_keyName};";
            return await _connection.ExecuteAsync(sql, entity);
        }

        public async Task<int> Delete(int id)
        {
            var sql = $"DELETE FROM {_tableName} WHERE {_keyName} = @id;";
            return await _connection.ExecuteAsync(sql, new { id });
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var selectCols = string.Join(", ", new[] { _keyName }.Concat(_columns));
            var sql = $"SELECT {selectCols} FROM {_tableName};";
            return await _connection.ExecuteQueryAsync<T>(sql);
        }

        public async Task<T?> GetById(int id)
        {
            var selectCols = string.Join(", ", new[] { _keyName }.Concat(_columns));
            var sql = $"SELECT {selectCols} FROM {_tableName} WHERE {_keyName} = @id;";
            return await _connection.ExecuteQueryFirstAsync<T>(sql, new { id });
        }
    }
}