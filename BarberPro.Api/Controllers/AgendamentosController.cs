using BarberPro.Application.Interfaces;
using BarberPro.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BarberPro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentosController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentosController(IAgendamentoService agendamentoService)
            => _agendamentoService = agendamentoService;

        [HttpGet]
        public async Task<IActionResult> Listar()
            => Ok(await _agendamentoService.ListarAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            var agendamento = await _agendamentoService.ObterPorIdAsync(id);
            if (agendamento == null) return NotFound();
            return Ok(agendamento);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Agendamento agendamento)
        {
            var criado = await _agendamentoService.CriarAsync(agendamento);
            return CreatedAtAction(nameof(Obter), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Agendamento agendamento)
        {
            if (id != agendamento.Id) return BadRequest();
            await _agendamentoService.AtualizarAsync(agendamento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _agendamentoService.RemoverAsync(id);
            return NoContent();
        }

        [HttpGet("disponiveis")]
        public async Task<IActionResult> BarbeirosDisponiveis([FromQuery] DateTime data)
            => Ok(await _agendamentoService.BarbeirosDisponiveisAsync(data));

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ListarPorUsuario(Guid usuarioId)
            => Ok(await _agendamentoService.ListarPorUsuarioAsync(usuarioId));

        [HttpGet("horarios-disponiveis")]
        public async Task<IActionResult> HorariosDisponiveis([FromQuery] DateTime data)
            => Ok(await _agendamentoService.HorariosDisponiveisPorDiaAsync(data));

    }

}
