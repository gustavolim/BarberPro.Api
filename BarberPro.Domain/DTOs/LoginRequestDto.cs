using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Domain.DTOs
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    // BarberPro.Domain/DTOs/LoginResponseDto.cs
    public class LoginResponseDto
    {
        public string Token { get; set; }
    }
}
