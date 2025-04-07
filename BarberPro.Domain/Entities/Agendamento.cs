using BarberPro.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Domain.Entities
{
    public class Agendamento : IEntidade
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid BarbeiroId { get; set; }
        public DateTime DataHora { get; set; }

        public Usuario Cliente { get; set; }
        public Usuario Barbeiro { get; set; }
    }

}
