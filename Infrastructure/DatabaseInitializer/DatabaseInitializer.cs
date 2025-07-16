using Dapper;
using HyperEfficient.Contracts.Infrastructure;
using MySqlConnector;

namespace HyperEfficient.Infrastructure.DatabaseInitializer
{
    public static class DatabaseInitializer
    {
        public static void EnsureDatabaseAndTablesCreated(IConnection connection, IConfiguration configuration)
        {
            MySqlConnectionStringBuilder builder = new(configuration.GetConnectionString("Default") ??
                                                       throw new ArgumentException(
                                                           "Não foi encontrada a string de conexão com o banco no seu AppSettings!"));
            builder.Database = "";

            var script = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "CreateSQL.txt"));

            using (MySqlConnection con = new(builder.ConnectionString))
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

                var eventSql = @"
                    CREATE EVENT IF NOT EXISTS fecha_registros_23h59
                    ON SCHEDULE EVERY 1 DAY
                    STARTS (TIMESTAMP(CURRENT_DATE, '23:59:00') + INTERVAL 0 DAY)
                    DO
                    BEGIN
                        UPDATE Registro
                        SET DataFinal = NOW()
                        WHERE DataFinal IS NULL;

                        INSERT INTO Registro (DataInicial, EquipamentoId)
                        SELECT NOW(), EquipamentoId
                        FROM Registro
                        WHERE DataFinal = NOW();
                    END
                ";
                con.Execute(eventSql);
            }
        }
    }
}