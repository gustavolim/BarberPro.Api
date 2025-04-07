using BarberPro.Api.Configuration;
using BarberPro.Application.Interfaces;
using BarberPro.Domain.Entities;
using Microsoft.Data.Sqlite;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do LINQ To DB
Linq2DbConfig.Configurar(builder.Configuration.GetConnectionString("BarberProDB"));
builder.Services.AddBarberProServices();

var app = builder.Build();

// Inicialização do banco de dados
await DatabaseInitializer.InicializarAsync(builder.Configuration.GetConnectionString("BarberProDB"));

// Seed do JSON
var seedPath = Path.Combine(AppContext.BaseDirectory, "Data", "seed-data.json");
if (File.Exists(seedPath))
{
    var json = await File.ReadAllTextAsync(seedPath);
    var seed = JsonSerializer.Deserialize<SeedData>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    if (seed is not null)
    {
        using var scope = app.Services.CreateScope();
        var usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();
        var agendamentoService = scope.ServiceProvider.GetRequiredService<IAgendamentoService>();

        var usuariosExistentes = await usuarioService.ListarAsync();
        if (!usuariosExistentes.Any())
        {
            foreach (var u in seed.Usuarios)
                await usuarioService.CriarAsync(u);

            foreach (var a in seed.Agendamentos)
                await agendamentoService.CriarAsync(a);
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Record de apoio para deserialização
record SeedData(List<Usuario> Usuarios, List<Agendamento> Agendamentos);
