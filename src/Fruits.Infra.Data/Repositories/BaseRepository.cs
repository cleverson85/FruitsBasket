using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using Fruits.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fruits.Infra.Data.Repositories
{
    public abstract class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : BaseEntity
    {
        protected readonly Context _context;
        protected readonly DbSet<Entity> _dbSet;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _context = (Context)unitOfWork.GetContext();
            _dbSet = _context.Set<Entity>();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dbSet.Remove(entity);
        }

        public virtual async Task<IList<Entity>> GetAll(PaginationParameterDto paginationParameter = null, params Expression<Func<Entity, object>>[] includes)
        {
            IQueryable<Entity> query = _dbSet;

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (paginationParameter != null)
            {
                return await query.OrderBy(c => c.Id)
                                  .Skip((paginationParameter.Page - 1) * paginationParameter.ItemsByPage)
                                  .Take(paginationParameter.ItemsByPage)
                                  .AsNoTracking()
                                  .ToListAsync();
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<IList<Entity>> GetByExpression(PaginationParameterDto paginationParameter, Expression<Func<Entity, bool>> filter = null,
            Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null, params Expression<Func<Entity, object>>[] includes)
        {
            IQueryable<Entity> query = _dbSet;

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.AsQueryable().Include(include);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query.AsQueryable());
            }

            if (paginationParameter != null)
            {
                query = query.Skip((paginationParameter.Page - 1) * paginationParameter.ItemsByPage)
                             .Take(paginationParameter.ItemsByPage)
                             .OrderBy(c => c.Id);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<Entity> GetById(int id, params Expression<Func<Entity, object>>[] includes)
        {
            IQueryable<Entity> query = _dbSet;

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var result = await query.FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public virtual async Task<Entity> Save(Entity entity)
        {
            if (entity.Id == 0)
            {
                await _dbSet.AddAsync(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }

            return entity;
        }

        public async Task<bool> SaveList(IList<Entity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return true;
        }

        public virtual async Task<Entity> Update(Entity entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }
    }
}
