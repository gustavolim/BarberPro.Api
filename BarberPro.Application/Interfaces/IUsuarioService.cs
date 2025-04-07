using BarberPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ListarAsync();
        Task<Usuario> ObterPorIdAsync(Guid id);
        Task<Usuario> CriarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task RemoverAsync(Guid id);
        Task<List<Usuario>> ListarBarbeirosAsync();
    }

}
