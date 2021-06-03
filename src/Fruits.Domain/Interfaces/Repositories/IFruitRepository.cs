using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fruits.Domain.Interfaces.Repositories
{
    public interface IFruitRepository : IBaseRepository<Fruit>
    {
        Task<IList<Fruit>> FindByName(string name, PaginationParameterDto paginationParameter);
    }
}
