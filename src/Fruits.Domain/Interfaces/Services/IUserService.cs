using Fruits.Domain.Dto;
using Fruits.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Fruits.Domain.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> Register(User user);
        Task<User> Authenticate(UserDto user);
        Task<User> FindById(int id);
    }
}
