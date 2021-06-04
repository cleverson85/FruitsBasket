using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fruits.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<Entity> where Entity : BaseEntity
    {
        Task<Entity> Save(Entity entity);
        Task<bool> SaveList(IList<Entity> entities);
        Task<Entity> Update(Entity entity);
        Task Delete(int id);
        Task<IList<Entity>> GetAll(PaginationParameterDto paginationParameter = null, params Expression<Func<Entity, object>>[] includes);
        Task<Entity> GetById(int id, params Expression<Func<Entity, object>>[] includes);
        Task<IList<Entity>> GetByExpression(PaginationParameterDto paginationParameter, Expression<Func<Entity, bool>> filter = null, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null,
            params Expression<Func<Entity, object>>[] includes);
        Task<int> Count();
    }
}
