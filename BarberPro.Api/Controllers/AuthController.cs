using BarberPro.Application.Interfaces;
using BarberPro.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BarberPro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            try
            {
                var token = await _authService.AutenticarAsync(request.Email, request.Senha);
                return Ok(new LoginResponseDto { Token = token });
            }
            catch
            {
                return Unauthorized("Credenciais inválidas.");
            }
        }

        [HttpPost("registro")]
        public async Task<ActionResult<RegistroResponseDto>> Registro([FromBody] RegistroUsuarioDto request)
        {
            try
            {
                var resultado = await _authService.RegistrarUsuarioAsync(request);
                return CreatedAtAction(nameof(Registro), resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao registrar usuário: {ex.Message}");
            }
        }
    }
}
