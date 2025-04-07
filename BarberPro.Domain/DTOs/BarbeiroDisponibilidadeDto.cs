using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Domain.DTOs
{
    public class BarbeiroDisponibilidadeDto
    {
        public Guid BarbeiroId { get; set; }
        public string Nome { get; set; }
        public List<string> HorariosDisponiveis { get; set; } = new();
    }
}
