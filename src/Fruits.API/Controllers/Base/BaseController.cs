using AutoMapper;
using Fruits.Application.Mappers;
using Fruits.Application.ViewModels;
using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Fruits.Domain.Util.Endpoints;

namespace Fruits.API.Controllers.Base
{
    [ApiController]
    [Helpers.Authorize]
    public abstract class BaseController<Entity, ViewModel> : ControllerBase where Entity : BaseEntity where ViewModel : BaseViewModel
    {
        private readonly IBaseService<Entity> _baseService;
        private readonly IUnitOfWork _unitOfWork;
        protected IMapper _mapper = AutoMapperConfiguration.GetMapper();

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
        public virtual async Task<IActionResult> Save([FromBody] ViewModel viewModel)
        {
            var result = _mapper.Map<Entity>(viewModel);
            await _baseService.Save(result);
            await _unitOfWork.Commit();

            return Ok(await GetAll(new PaginationParameterDto()));
        }

        [HttpPost]
        [Route(Route.LIST)]
        public virtual async Task<IActionResult> Save([FromBody] IList<ViewModel> viewModel)
        {
            var result = _mapper.Map<IList<Entity>>(viewModel);
            await _baseService.Save(result);
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
