using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fruits.Domain.Interfaces.Services
{
    public interface IFruitService : IBaseService<Fruit>
    {
        Task<IList<Fruit>> FindByName(string name, PaginationParameterDto paginationParameter);
    }
}
