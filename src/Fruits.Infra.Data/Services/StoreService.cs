using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fruits.Infra.Data.Services
{
    public class StoreService : BaseService<Store>, IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IFruitService _fruitService;

        public StoreService(IStoreRepository storeRepository, IFruitService fruitService) : base(storeRepository)
        {
            _storeRepository = storeRepository;
            _fruitService = fruitService;
        }

        public override async Task<bool> Save(IList<Store> stores)
        {
            if (await _fruitService.ValidateQuatity(stores))
            {
                return await _storeRepository.SaveList(stores);
            }

            return false;
        }

        public override Task<IList<Store>> GetAll(PaginationParameterDto paginationParameter = null)
        {
            return _storeRepository.GetAll();
        }
    }
}
