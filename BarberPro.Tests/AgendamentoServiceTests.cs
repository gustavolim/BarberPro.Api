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
    public class AgendamentoServiceTests
    {
        private readonly Mock<IAgendamentoRepository> _mockRepo;
        private readonly AgendamentoService _service;

        public AgendamentoServiceTests()
        {
            _mockRepo = new Mock<IAgendamentoRepository>();
            _service = new AgendamentoService(_mockRepo.Object);
        }

        [Fact]
        public async Task CriarAsync_DeveRetornarAgendamentoCriado()
        {
            var agendamento = new Agendamento
            {
                Id = Guid.NewGuid(),
                ClienteId = Guid.NewGuid(),
                BarbeiroId = Guid.NewGuid(),
                DataHora = DateTime.Now
            };

            _mockRepo.Setup(r => r.CriarAsync(agendamento)).ReturnsAsync(agendamento);

            var resultado = await _service.CriarAsync(agendamento);

            Assert.Equal(agendamento, resultado);
            _mockRepo.Verify(r => r.CriarAsync(agendamento), Times.Once);
        }

        [Fact]
        public async Task BarbeirosDisponiveisAsync_DeveChamarRepositorio()
        {
            var data = DateTime.Today;
            _mockRepo.Setup(r => r.BarbeirosDisponiveisAsync(data)).ReturnsAsync(new List<Usuario>());

            var resultado = await _service.BarbeirosDisponiveisAsync(data);

            Assert.NotNull(resultado);
            _mockRepo.Verify(r => r.BarbeirosDisponiveisAsync(data), Times.Once);
        }
    }
}
