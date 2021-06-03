using Fruits.Domain.Models;
using System.Threading.Tasks;

namespace Fruits.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> FindUser(string email);
    }
}
