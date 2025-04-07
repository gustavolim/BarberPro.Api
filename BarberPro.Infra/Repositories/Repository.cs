using BarberPro.Domain.Interfaces;
using BarberPro.Infra.Data;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberPro.Infra.Repositories
{
    // BarberPro.Infra/Repositories/Repository.cs
    public class Repository<T> : IRepository<T> where T : class, IEntidade
    {
        protected readonly DatabaseContext _db;

        public Repository(DatabaseContext db) => _db = db;

        public virtual async Task<List<T>> ListarAsync() => await _db.GetTable<T>().ToListAsync();

        public virtual async Task<T> ObterPorIdAsync(Guid id) => await _db.GetTable<T>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> CriarAsync(T entidade)
        {
            await _db.InsertAsync(entidade);
            return entidade;
        }

        public async Task AtualizarAsync(T entidade) => await _db.UpdateAsync(entidade);

        public async Task RemoverAsync(Guid id)
        {
            var item = await ObterPorIdAsync(id);
            if (item != null) await _db.DeleteAsync(item);
        }
    }
}
