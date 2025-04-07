using BarberPro.Domain.DTOs;
using BarberPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Domain.Interfaces
{
    public interface IAgendamentoRepository : IRepository<Agendamento>
    {
        Task<List<Usuario>> BarbeirosDisponiveisAsync(DateTime data);
        Task<List<Agendamento>> ListarPorUsuarioIdAsync(Guid usuarioId);
        Task<List<BarbeiroDisponibilidadeDto>> HorariosDisponiveisPorDiaAsync(DateTime data);
    }
}
