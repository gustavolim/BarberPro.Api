using System.Text.Json;
using BarberPro.Domain.Entities;
using BarberPro.Infra.Data;
using LinqToDB;
using LinqToDB.Data;

public static class SeedLoader
{
    public class SeedData
    {
        public List<Usuario> Usuarios { get; set; } = new();
        public List<Agendamento> Agendamentos { get; set; } = new();
    }

    public static async Task PopularAsync(DatabaseContext db, string jsonPath)
    {
        if (!File.Exists(jsonPath)) return;

        var json = await File.ReadAllTextAsync(jsonPath);
        var seed = JsonSerializer.Deserialize<SeedData>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (!(await db.Usuarios.AnyAsync()))
            await db.BulkCopyAsync(seed.Usuarios);

        if (!(await db.Agendamentos.AnyAsync()))
            await db.BulkCopyAsync(seed.Agendamentos);
    }
}
