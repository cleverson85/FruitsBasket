using Fruits.Domain.Dto;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Fruits.Domain.Util.Endpoints;

namespace Fruits.API.Controllers
{
    [ApiController]
    [Route(Recursos.Authentication)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthJwtService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthJwtService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        [Route(Route.LOGIN)]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {
            var userAuthenticated = await _userService.Authenticate(user);

            if (userAuthenticated == null)
            {
                return BadRequest();
            }

            string token = await _authService.GenerateToken(userAuthenticated);

            return Ok(new AuthResponse { Token = token, IsAuthenticaded = true });
        }
    }
}
