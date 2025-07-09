using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Repositories.Base;
using HyperEfficient.Entities;
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
                .Where(p => !string.Equals(p.Name, _keyName, StringComparison.OrdinalIgnoreCase))
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

    public class EquipamentoRepository : RepositoryBase<Equipamento>, IEquipamentoRepository
    {
        public EquipamentoRepository(IConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Equipamento>> GetPaged(int page, int pageSize)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            var offset = (page - 1) * pageSize;
            var type = typeof(Equipamento);
            var keyName = "Id";
            var cols = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name);
            var selectCols = string.Join(", ", cols);

            var sql = $@"
                SELECT {selectCols}
                FROM EQUIPAMENTO
                LIMIT @pageSize OFFSET @offset;";
            return await _connection.ExecuteQueryAsync<Equipamento>(sql, new { pageSize, offset });
        }
    }
}