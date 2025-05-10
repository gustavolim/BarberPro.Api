using System.ComponentModel.DataAnnotations;

namespace BarberPro.Domain.DTOs
{
    public class RegistroUsuarioDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; } = null!;

        public bool EhBarbeiro { get; set; } = false;
    }

    public class RegistroResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EhBarbeiro { get; set; }
    }
}
