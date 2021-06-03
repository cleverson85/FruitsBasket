using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fruits.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
        void Rollback();
        DbContext GetContext();
    }
}