using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fruits.Domain.Interfaces.Services
{
    public interface IBaseService<Entity> where Entity : BaseEntity
    {
        Task<Entity> Save(Entity entity);
        Task<bool> Save(IList<Entity> entities);
        Task Delete(int id);
        Task<IList<Entity>> GetAll(PaginationParameterDto paginationParameter = null);
        Task<Entity> GetById(int id);
        Task<IList<Entity>> GetByExpression(PaginationParameterDto paginationParameter, Expression<Func<Entity, bool>> filter = null, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, 
            params Expression<Func<Entity, object>>[] includes);
        Task<int> Count();
    }
}
