using BarberPro.Application.Interfaces;
using BarberPro.Api.Controllers;
using BarberPro.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BarberPro.Tests
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTest()
        {
            _mockAuthService = new Mock<IAuthService>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Registro_ComDadosValidos_RetornaCreatedAtAction()
        {
            // Arrange
            var registroDto = new RegistroUsuarioDto
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Senha = "Senha123!"
            };
            var responseDto = new RegistroResponseDto
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Email = "teste@example.com"
            };

            _mockAuthService
                .Setup(s => s.RegistrarUsuarioAsync(It.IsAny<RegistroUsuarioDto>()))
                .ReturnsAsync(responseDto);

            // Act
            var result = await _controller.Registro(registroDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(AuthController.Registro), createdAtActionResult.ActionName);
            Assert.Equal(responseDto, createdAtActionResult.Value);
        }

        [Fact]
        public async Task Registro_QuandoInvalidOperationException_RetornaBadRequest()
        {
            // Arrange
            var registroDto = new RegistroUsuarioDto
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Senha = "Senha123!"
            };
            var mensagemErro = "Email já cadastrado.";

            _mockAuthService
                .Setup(s => s.RegistrarUsuarioAsync(It.IsAny<RegistroUsuarioDto>()))
                .ThrowsAsync(new InvalidOperationException(mensagemErro));

            // Act
            var result = await _controller.Registro(registroDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(mensagemErro, badRequestResult.Value);
        }

        [Fact]
        public async Task Registro_QuandoExcecaoGenerica_RetornaStatusCode500()
        {
            // Arrange
            var registroDto = new RegistroUsuarioDto
            {
                Nome = "Teste",
                Email = "teste@example.com",
                Senha = "Senha123!"
            };
            var mensagemErro = "Erro interno do servidor";

            _mockAuthService
                .Setup(s => s.RegistrarUsuarioAsync(It.IsAny<RegistroUsuarioDto>()))
                .ThrowsAsync(new Exception(mensagemErro));

            // Act
            var result = await _controller.Registro(registroDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal($"Erro ao registrar usuário: {mensagemErro}", statusCodeResult.Value);
        }
    }
}
