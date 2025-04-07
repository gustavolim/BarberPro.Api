using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IEntidade
    {
        Task<T> ObterPorIdAsync(Guid id);
        Task<List<T>> ListarAsync();
        Task<T> CriarAsync(T entidade);
        Task AtualizarAsync(T entidade);
        Task RemoverAsync(Guid id);
    }

}
