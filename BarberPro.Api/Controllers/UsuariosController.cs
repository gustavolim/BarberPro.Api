using BarberPro.Application.Interfaces;
using BarberPro.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BarberPro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
            => _usuarioService = usuarioService;

        [HttpGet]
        public async Task<IActionResult> Listar()
            => Ok(await _usuarioService.ListarAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Usuario usuario)
        {
            var criado = await _usuarioService.CriarAsync(usuario);
            return CreatedAtAction(nameof(Obter), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest();
            await _usuarioService.AtualizarAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _usuarioService.RemoverAsync(id);
            return NoContent();
        }

        [HttpGet("barbeiros")]
        public async Task<IActionResult> ListarBarbeiros()
            => Ok(await _usuarioService.ListarBarbeirosAsync());
    }

}
