using BarberPro.Domain.DTOs;
using BarberPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Application.Interfaces
{
    public interface IAgendamentoService
    {
        Task<List<Agendamento>> ListarAsync();
        Task<Agendamento> ObterPorIdAsync(Guid id);
        Task<Agendamento> CriarAsync(Agendamento agendamento);
        Task AtualizarAsync(Agendamento agendamento);
        Task RemoverAsync(Guid id);
        Task<List<Usuario>> BarbeirosDisponiveisAsync(DateTime data);
        Task<List<Agendamento>> ListarPorUsuarioAsync(Guid usuarioId);
        Task<List<BarbeiroDisponibilidadeDto>> HorariosDisponiveisPorDiaAsync(DateTime data);
    }
}
