using Fruits.Domain.Interfaces;
using Fruits.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fruits.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public DbContext GetContext()
        {
            return _context;
        }

        public async Task<int> Commit()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public void Rollback()
        { }
    }
}
