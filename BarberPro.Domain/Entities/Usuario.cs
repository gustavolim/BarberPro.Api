using BarberPro.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Domain.Entities
{
    public class Usuario : IEntidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool EhBarbeiro { get; set; }
        public List<Agendamento> Agendamentos { get; set; } = new();
    }
}
