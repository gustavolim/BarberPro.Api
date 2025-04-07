using BarberPro.Domain.Entities;
using LinqToDB;
using LinqToDB.Data;

namespace BarberPro.Infra.Data
{
    public class DatabaseContext : DataConnection
    {
        public DatabaseContext() : base("BarberProDB") { }

        public ITable<Usuario> Usuarios => this.GetTable<Usuario>();
        public ITable<Agendamento> Agendamentos => this.GetTable<Agendamento>();
    }
}
