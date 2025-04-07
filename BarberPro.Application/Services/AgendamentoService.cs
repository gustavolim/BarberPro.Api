using BarberPro.Application.Interfaces;
using BarberPro.Domain.DTOs;
using BarberPro.Domain.Entities;
using BarberPro.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Application.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _repo;

        public AgendamentoService(IAgendamentoRepository repo) => _repo = repo;

        public Task<List<Agendamento>> ListarAsync() => _repo.ListarAsync();
        public Task<Agendamento> ObterPorIdAsync(Guid id) => _repo.ObterPorIdAsync(id);
        public Task<Agendamento> CriarAsync(Agendamento agendamento) => _repo.CriarAsync(agendamento);
        public Task AtualizarAsync(Agendamento agendamento) => _repo.AtualizarAsync(agendamento);
        public Task RemoverAsync(Guid id) => _repo.RemoverAsync(id);
        public Task<List<Usuario>> BarbeirosDisponiveisAsync(DateTime data) => _repo.BarbeirosDisponiveisAsync(data);
        public Task<List<Agendamento>> ListarPorUsuarioAsync(Guid usuarioId) => _repo.ListarPorUsuarioIdAsync(usuarioId);

        public Task<List<BarbeiroDisponibilidadeDto>> HorariosDisponiveisPorDiaAsync(DateTime data) => _repo.HorariosDisponiveisPorDiaAsync(data);
    }
}
