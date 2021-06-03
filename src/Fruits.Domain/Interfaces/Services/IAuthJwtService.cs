using Fruits.Domain.Models;
using System.Threading.Tasks;

namespace Fruits.Domain.Interfaces.Services
{
    public interface IAuthJwtService
    {
        Task<string> GenerateToken(User user);
    }
}
