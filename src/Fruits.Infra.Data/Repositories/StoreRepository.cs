using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Models;
using Fruits.Domain.Searching;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fruits.Infra.Data.Repositories
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public override Task<IList<Store>> GetAll(PaginationParameterDto paginationParameter = null, params Expression<Func<Store, object>>[] includes)
        {
            return base.GetAll(paginationParameter, c => c.Fruit);
        }
    }
}
