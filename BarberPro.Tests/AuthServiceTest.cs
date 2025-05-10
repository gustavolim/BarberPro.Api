using BarberPro.Application.Services;
using BarberPro.Domain.DTOs;
using BarberPro.Domain.Entities;
using BarberPro.Domain.Interfaces;
using Moq;

namespace BarberPro.Tests
{
    public class AuthServiceTest
    {
        private readonly Mock<IUsuarioRepository> _mockRepo;
        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _mockRepo = new Mock<IUsuarioRepository>();
            _authService = new AuthService(_mockRepo.Object);
        }

        [Fact]
        public async Task RegistrarUsuarioAsync_ComEmailUnico_DeveRetornarUsuarioCriado()
        {
            // Arrange
            var registroDto = new RegistroUsuarioDto
            {
                Nome = "Teste",
                Email = "teste@email.com",
                Senha = "Senha123",
                EhBarbeiro = false
            };

            var usuarios = new List<Usuario>();
            _mockRepo.Setup(r => r.ListarAsync()).ReturnsAsync(usuarios);
            
            _mockRepo.Setup(r => r.CriarAsync(It.IsAny<Usuario>()))
                    .ReturnsAsync((Usuario u) => u);

            // Act
            var resultado = await _authService.RegistrarUsuarioAsync(registroDto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(registroDto.Nome, resultado.Nome);
            Assert.Equal(registroDto.Email, resultado.Email);
            Assert.Equal(registroDto.EhBarbeiro, resultado.EhBarbeiro);
            Assert.NotEqual(Guid.Empty, resultado.Id);
            _mockRepo.Verify(r => r.CriarAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task RegistrarUsuarioAsync_ComEmailJaCadastrado_DeveLancarInvalidOperationException()
        {
            // Arrange
            var email = "existente@email.com";
            var registroDto = new RegistroUsuarioDto
            {
                Nome = "Teste",
                Email = email,
                Senha = "Senha123",
                EhBarbeiro = false
            };

            var usuarioExistente = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Usuário Existente",
                Email = email,
                SenhaHash = "hash",
                EhBarbeiro = true
            };

            var usuarios = new List<Usuario> { usuarioExistente };
            _mockRepo.Setup(r => r.ListarAsync()).ReturnsAsync(usuarios);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _authService.RegistrarUsuarioAsync(registroDto));
            
            Assert.Equal("Email já cadastrado.", exception.Message);
            _mockRepo.Verify(r => r.CriarAsync(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public async Task RegistrarUsuarioAsync_DeveCriarHashDaSenha()
        {
            // Arrange
            var senha = "Senha123";
            var registroDto = new RegistroUsuarioDto
            {
                Nome = "Teste",
                Email = "teste@email.com",
                Senha = senha,
                EhBarbeiro = false
            };

            var usuarios = new List<Usuario>();
            _mockRepo.Setup(r => r.ListarAsync()).ReturnsAsync(usuarios);
            
            Usuario usuarioCriado = null!;
            _mockRepo.Setup(r => r.CriarAsync(It.IsAny<Usuario>()))
                    .Callback<Usuario>(u => usuarioCriado = u)
                    .ReturnsAsync((Usuario u) => u);

            // Act
            await _authService.RegistrarUsuarioAsync(registroDto);

            // Assert
            Assert.NotNull(usuarioCriado);
            Assert.NotEqual(senha, usuarioCriado.SenhaHash);
            Assert.Equal(AuthService.GerarHash(senha), usuarioCriado.SenhaHash);
        }
    }
}
