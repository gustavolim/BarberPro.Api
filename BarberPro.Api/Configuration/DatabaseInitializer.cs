using BarberPro.Infra.Data;
using LinqToDB;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

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

            using var context = new DatabaseContext();
            var usuarios = await context.Usuarios.ToListAsync();
            foreach (var usuario in usuarios)
            {
                if (string.IsNullOrWhiteSpace(usuario.SenhaHash))
                {
                    usuario.SenhaHash = GerarHash("123456"); 
                    await context.UpdateAsync(usuario);
                }
            }
        }

        private static string GerarHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

}
