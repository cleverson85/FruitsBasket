using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using static Fruits.Domain.Util.Endpoints;

namespace Fruits.API.Controllers
{
    [Helpers.Authorize]
    [ApiController]
    [Route(Recursos.Store)]
    public class StoreController : BaseController<Store>
    {
        private readonly IStoreService _storeService;
        private readonly IUnitOfWork _unitOfWork;

        public StoreController(IStoreService storeService, IUnitOfWork unitOfWork) : base(storeService, unitOfWork)
        {
            _storeService = storeService;
            _unitOfWork = unitOfWork;
        }
    }
}