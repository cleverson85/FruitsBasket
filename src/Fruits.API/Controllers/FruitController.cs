using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Fruits.Domain.Util.Endpoints;

namespace Fruits.API.Controllers
{
    [Helpers.Authorize]
    [ApiController]
    [Route(Recursos.Fruit)]
    public class FruitController : BaseController<Fruit>
    {
        private readonly IFruitService _fruitService;
        private readonly IUnitOfWork _unitOfWork;

        public FruitController(IFruitService fruitService, IUnitOfWork unitOfWork) : base(fruitService, unitOfWork)
        {
            _fruitService = fruitService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route(Route.NAME)]
        public async Task<IActionResult> FindByName(string name, [FromQuery] PaginationParameterDto paginationParameter)
        {
            var result = await _fruitService.FindByName(name, paginationParameter);
            return Ok(new Result<Fruit>(result, result.Count));
        }
    }
}
