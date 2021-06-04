using Fruits.Domain.Dto;
using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using System;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Fruits.Infra.Data.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Register(User user)
        {
            string hashPassWord = BC.HashPassword(user.Senha);
            user.Senha = hashPassWord;

            return await Save(user);
        }

        public async Task<User> Authenticate(UserDto user)
        {
            var account = await _userRepository.FindUser(user.Email);

            if (account == null || !BC.Verify(user.Senha, account.Senha))
            {
                return null;
            }

            return account;
        }

        public async Task<User> FindById(int id)
        {
            return await _userRepository.GetById(id);
        }
    }
}
