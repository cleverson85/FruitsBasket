using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Fruits.Domain.Util.Endpoints;

namespace Fruits.API.Controllers
{
    [ApiController]
    [Route(Recursos.Account)]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route(Route.REGISTER)]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var account = await _userService.Register(user);
            await _unitOfWork.Commit();
            return Ok(account);
        }
    }
}
