using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
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

        public override async Task<bool> Save(IList<Store> entity)
        {
            IList<Store> stores = new List<Store>();

            foreach (var item in entity)
            {
                stores.Add(new Store()
                {
                    FruitId = item.Fruit.Id,
                    Quantity = item.Quantity,
                    TotalValue = item.TotalValue
                });
            }

            if (await _fruitService.ValidateQuatity(stores))
            {
                return await _storeRepository.SaveList(stores);
            }

            return false;
        }
    }
}
