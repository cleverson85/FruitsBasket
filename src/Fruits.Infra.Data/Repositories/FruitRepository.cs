using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fruits.Infra.Data.Repositories
{
    public class FruitRepository : BaseRepository<Fruit>, IFruitRepository
    {
        public FruitRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<IList<Fruit>> FindByName(string name, PaginationParameterDto paginationParameter)
        {
            return await GetByExpression(paginationParameter, c => c.Name.ToLower().Contains(name.ToLower()), c => c.OrderBy(e => e.Name));
        }
    }
}
