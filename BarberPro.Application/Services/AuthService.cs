using BarberPro.Application.Interfaces;
using BarberPro.Domain.DTOs;
using BarberPro.Domain.Entities;
using BarberPro.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _repo;

        public AuthService(IUsuarioRepository repo) => _repo = repo;

        public async Task<string> AutenticarAsync(string email, string senha)
        {
            var usuario = (await _repo.ListarAsync())
                .FirstOrDefault(u => u.Email == email);

            if (usuario == null || usuario.SenhaHash != GerarHash(senha))
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            // Aqui você pode gerar e retornar um JWT no futuro.
            return "usuario-autenticado"; // Placeholder para token
        }

        public async Task<RegistroResponseDto> RegistrarUsuarioAsync(RegistroUsuarioDto registro)
        {
            var usuarioExistente = (await _repo.ListarAsync())
                .FirstOrDefault(u => u.Email == registro.Email);

            if (usuarioExistente != null)
                throw new InvalidOperationException("Email já cadastrado.");

            var novoUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = registro.Nome,
                Email = registro.Email,
                EhBarbeiro = registro.EhBarbeiro,
                SenhaHash = GerarHash(registro.Senha)
            };

            var usuarioCriado = await _repo.CriarAsync(novoUsuario);

            return new RegistroResponseDto
            {
                Id = usuarioCriado.Id,
                Nome = usuarioCriado.Nome,
                Email = usuarioCriado.Email,
                EhBarbeiro = usuarioCriado.EhBarbeiro
            };
        }

        public static string GerarHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
