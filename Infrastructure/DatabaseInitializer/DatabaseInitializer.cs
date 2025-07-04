using Dapper;
using HyperEfficient.Contracts.Infrastructure;
using MySqlConnector;

namespace HyperEfficient.Infrastructure.DatabaseInitializer
{
    public static class DatabaseInitializer
    {
        public static void EnsureDatabaseAndTablesCreated(IConnection connection, IConfiguration configuration)
        {
            MySqlConnectionStringBuilder builder = new(configuration.GetConnectionString("Default") ?? throw new ArgumentException("Não foi encontrada a string de conexão com o banco no seu AppSettings!"));
            builder.Database = "";

            string script = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "CreateSQL.txt"));

            using (MySqlConnection con = new(builder.ConnectionString))
            {
                con.Open();

                foreach (string cmd in script.Split(';'))
                {
                    string trimmed = cmd.Trim();

                    if (!string.IsNullOrWhiteSpace(trimmed))
                    {
                        con.Execute(trimmed);
                    }
                }
            }
        }
    }
}