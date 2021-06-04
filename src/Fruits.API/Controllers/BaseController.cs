using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Fruits.Domain.Util.Endpoints;

namespace Fruits.API.Controllers
{
    [Helpers.Authorize]
    [ApiController]
    public abstract class BaseController<Entity> : ControllerBase where Entity : BaseEntity
    {
        private readonly IBaseService<Entity> _baseService;
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IBaseService<Entity> baseService, IUnitOfWork unitOfWork)
        {
            _baseService = baseService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route(Route.ALL)]
        public virtual async Task<IActionResult> GetAll([FromQuery] PaginationParameterDto paginationParameter)
        {
            var result = await _baseService.GetAll(paginationParameter);
            var count = await _baseService.Count();
            return Ok(new Result<Entity>(result, count));
        }

        [HttpGet]
        [Route(Route.ID)]
        public virtual async Task<IActionResult> FindById(int id)
        {
            var result = await _baseService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Route(Route.POST)]
        public virtual async Task<IActionResult> Save([FromBody] Entity entity)
        {
            await _baseService.Save(entity);
            await _unitOfWork.Commit();
            return Ok(await GetAll(new PaginationParameterDto()));
        }

        [HttpPost]
        [Route(Route.LIST)]
        public virtual async Task<IActionResult> Save([FromBody] IList<Entity> entity)
        {
            await _baseService.Save(entity);
            await _unitOfWork.Commit();
            return Ok(await GetAll(new PaginationParameterDto()));
        }

        [HttpDelete]
        [Route(Route.DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            await _baseService.Delete(id);
            await _unitOfWork.Commit();
            return Ok(await GetAll(new PaginationParameterDto()));
        }
    }
}
