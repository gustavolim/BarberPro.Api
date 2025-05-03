using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> AutenticarAsync(string email, string senha);
    }
}
