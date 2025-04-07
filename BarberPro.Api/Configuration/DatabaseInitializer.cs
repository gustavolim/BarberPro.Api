using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace BarberPro.Api.Configuration
{
    public static class DatabaseInitializer
    {
        public static async Task InicializarAsync(string connectionString)
        {
            var builder = new SqliteConnectionStringBuilder(connectionString);
            var dbPath = builder.DataSource;

            if (!File.Exists(dbPath))
                File.Create(dbPath).Dispose();

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var sql = await File.ReadAllTextAsync("Configuration/init_barberpro_schema.sql");

            var command = connection.CreateCommand();
            command.CommandText = sql;
            await command.ExecuteNonQueryAsync();
        }
    }

}
