using BarberPro.Domain.DTOs;
using BarberPro.Domain.Entities;
using BarberPro.Domain.Interfaces;
using BarberPro.Infra.Data;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Infra.Repositories
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(DatabaseContext db) : base(db) { }

        public override async Task<Agendamento> ObterPorIdAsync(Guid id)
        {
            var query = from a in _db.Agendamentos
                        join c in _db.Usuarios on a.ClienteId equals c.Id
                        join b in _db.Usuarios on a.BarbeiroId equals b.Id
                        where a.Id == id
                        select new Agendamento
                        {
                            Id = a.Id,
                            ClienteId = a.ClienteId,
                            BarbeiroId = a.BarbeiroId,
                            DataHora = a.DataHora,
                            Cliente = c,
                            Barbeiro = b
                        };

            return await query.FirstOrDefaultAsync();
        }
        public override async Task<List<Agendamento>> ListarAsync()
        {
            var query = from a in _db.Agendamentos
                        join c in _db.Usuarios on a.ClienteId equals c.Id
                        join b in _db.Usuarios on a.BarbeiroId equals b.Id
                        select new Agendamento
                        {
                            Id = a.Id,
                            ClienteId = a.ClienteId,
                            BarbeiroId = a.BarbeiroId,
                            DataHora = a.DataHora,
                            Cliente = c,
                            Barbeiro = b
                        };

            return await query.ToListAsync();
        }
        public async Task<List<Usuario>> BarbeirosDisponiveisAsync(DateTime data)
        {
            var agendados = await _db.Agendamentos
                .Where(a => a.DataHora == data)
                .Select(a => a.BarbeiroId)
                .ToListAsync();

            return await _db.Usuarios
                .Where(u => u.EhBarbeiro && !agendados.Contains(u.Id))
                .ToListAsync();
        }



        public async Task<List<Agendamento>> ListarPorUsuarioIdAsync(Guid usuarioId)
        {
            var query = from a in _db.Agendamentos
                        join c in _db.Usuarios on a.ClienteId equals c.Id
                        join b in _db.Usuarios on a.BarbeiroId equals b.Id
                        where a.ClienteId == usuarioId || a.BarbeiroId == usuarioId
                        select new Agendamento
                        {
                            Id = a.Id,
                            ClienteId = a.ClienteId,
                            BarbeiroId = a.BarbeiroId,
                            DataHora = a.DataHora,
                            Cliente = c,
                            Barbeiro = b
                        };

            return await query.ToListAsync();
        }

        public async Task<List<BarbeiroDisponibilidadeDto>> HorariosDisponiveisPorDiaAsync(DateTime data)
        {
            var inicio = data.Date.AddHours(9);  // 09:00
            var fim = data.Date.AddHours(17);    // 17:00

            var horariosPadrao = Enumerable.Range(0, (int)(fim - inicio).TotalHours)
                .Select(h => inicio.AddHours(h))
                .ToList();

            var barbeiros = await _db.Usuarios.Where(u => u.EhBarbeiro).ToListAsync();

            var agendamentosDoDia = await _db.Agendamentos
                .Where(a => a.DataHora.Date == data.Date)
                .ToListAsync();

            var resultado = new List<BarbeiroDisponibilidadeDto>();

            foreach (var barbeiro in barbeiros)
            {
                var agendados = agendamentosDoDia
                    .Where(a => a.BarbeiroId == barbeiro.Id)
                    .Select(a => a.DataHora)
                    .ToHashSet();

                var disponiveis = horariosPadrao
                    .Where(h => !agendados.Contains(h))
                    .Select(h => h.ToString("HH:mm"))
                    .ToList();

                resultado.Add(new BarbeiroDisponibilidadeDto
                {
                    BarbeiroId = barbeiro.Id,
                    Nome = barbeiro.Nome,
                    HorariosDisponiveis = disponiveis
                });
            }

            return resultado;
        }

    }
}
