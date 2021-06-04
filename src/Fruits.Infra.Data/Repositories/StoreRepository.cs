using Fruits.Domain.Interfaces;
using Fruits.Domain.Interfaces.Repositories;
using Fruits.Domain.Models;

namespace Fruits.Infra.Data.Repositories
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
