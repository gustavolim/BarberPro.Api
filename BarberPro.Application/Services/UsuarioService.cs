using BarberPro.Application.Interfaces;
using BarberPro.Domain.Entities;
using BarberPro.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo) => _repo = repo;

        public Task<List<Usuario>> ListarAsync() => _repo.ListarAsync();
        public Task<Usuario> ObterPorIdAsync(Guid id) => _repo.ObterPorIdAsync(id);
        public Task<Usuario> CriarAsync(Usuario usuario) => _repo.CriarAsync(usuario);
        public Task AtualizarAsync(Usuario usuario) => _repo.AtualizarAsync(usuario);
        public Task RemoverAsync(Guid id) => _repo.RemoverAsync(id);
        public async Task<List<Usuario>> ListarBarbeirosAsync()
        {
            var todos = await _repo.ListarAsync();
            return todos.Where(u => u.EhBarbeiro).ToList();
        }
    }
}
