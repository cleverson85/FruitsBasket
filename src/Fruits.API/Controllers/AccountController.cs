using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Fruits.Domain.Util.Endpoints;

namespace Fruits.API.Controllers
{
    [ApiController]
    [Route("api/account/[action]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route(Route.POST)]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var account = await _userService.Register(user);
            return Ok(account);
        }
    }
}
