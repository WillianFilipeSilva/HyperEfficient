using Dapper;
using HyperEfficient.Contracts.Infrastructure;
using MySqlConnector;

namespace HyperEfficient.Infrastructure.DatabaseInitializer;

public static class DatabaseInitializer
{
    public static void EnsureDatabaseAndTablesCreated(IConnection connection, IConfiguration configuration)
    {
        var builder = new MySqlConnectionStringBuilder(
            configuration.GetConnectionString("Default")
            ?? throw new ArgumentException("Não foi encontrada a string de conexão com o banco no seu AppSettings!")
        );
        builder.Database = "";

        var script = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "CreateSQL.txt"));
        using (var con = new MySqlConnection(builder.ConnectionString))
        {
            con.Open();
            foreach (var cmd in script.Split(';'))
            {
                var trimmed = cmd.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    con.Execute(trimmed);
                }
            }
        }
    }
}
