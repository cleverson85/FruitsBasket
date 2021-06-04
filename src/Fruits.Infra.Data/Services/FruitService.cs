using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fruits.Infra.Data.Services
{
    public class FruitService : BaseService<Fruit>, IFruitService
    {
        private readonly IFruitRepository _fruitRepository;

        public FruitService(IFruitRepository fruitRepository) : base(fruitRepository)
        {
            _fruitRepository = fruitRepository;
        }

        public async Task<IList<Fruit>> FindByName(string name, PaginationParameterDto paginationParameter)
        {
            return await _fruitRepository.FindByName(Uri.UnescapeDataString(name), paginationParameter);
        }

        public async Task<bool> ValidateQuatity(IList<Store> stores)
        {
            foreach (var item in stores)
            {
                var result = await _fruitRepository.GetById(item.FruitId);
                if (item.Quantity > result.AvailableQuantity)
                {
                    new Exception($"Quantidade informada do item {item.FruitId} não pode ser maior que a quantidade disponível.");
                }

                result.AvailableQuantity -= item.Quantity;
                await _fruitRepository.Save(result);
            }

            return true;
        }
    }
}
