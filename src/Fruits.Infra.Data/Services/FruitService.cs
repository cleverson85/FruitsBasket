using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Fruits.Domain.Searching;
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
            return await _fruitRepository.FindByName(name, paginationParameter);
        }
    }
}
