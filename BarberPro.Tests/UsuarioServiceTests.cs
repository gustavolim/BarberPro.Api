using BarberPro.Application.Services;
using BarberPro.Domain.Entities;
using BarberPro.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Tests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _mockRepo;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _mockRepo = new Mock<IUsuarioRepository>();
            _service = new UsuarioService(_mockRepo.Object);
        }

        [Fact]
        public async Task CriarAsync_DeveRetornarUsuarioCriado()
        {
            var usuario = new Usuario { Id = Guid.NewGuid(), Nome = "João" };
            _mockRepo.Setup(r => r.CriarAsync(usuario)).ReturnsAsync(usuario);

            var resultado = await _service.CriarAsync(usuario);

            Assert.Equal(usuario, resultado);
            _mockRepo.Verify(r => r.CriarAsync(usuario), Times.Once);
        }

        [Fact]
        public async Task ListarBarbeirosAsync_DeveRetornarSomenteBarbeiros()
        {
            var usuarios = new List<Usuario>
        {
            new Usuario { Id = Guid.NewGuid(), Nome = "Barbeiro 1", EhBarbeiro = true },
            new Usuario { Id = Guid.NewGuid(), Nome = "Cliente 1", EhBarbeiro = false }
        };

            _mockRepo.Setup(r => r.ListarAsync()).ReturnsAsync(usuarios);

            var barbeiros = await _service.ListarBarbeirosAsync();

            Assert.Single(barbeiros);
            Assert.All(barbeiros, u => Assert.True(u.EhBarbeiro));
        }
    }
}
