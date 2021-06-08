using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Fruits.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<User> FindUser(string email)
        {
            var result = await GetByExpression(null, c => c.Email.ToLower() == email.ToLower(), null);
            return result.FirstOrDefault();
        }
    }
}
