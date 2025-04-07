using BarberPro.Domain.Entities;
using BarberPro.Domain.Interfaces;
using BarberPro.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Infra.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DatabaseContext db) : base(db) { }
    }

}
