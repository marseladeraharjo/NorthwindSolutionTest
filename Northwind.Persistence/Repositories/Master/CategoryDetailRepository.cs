using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Entities.Master;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    public class CategoryDetailRepository : RepositoryBase<CategoryDetail>, IRepositoryEntityBase<CategoryDetail>
    {
        public CategoryDetailRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(CategoryDetail entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CategoryDetail entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CategoryDetail>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.Id).ToListAsync();
        }

        public Task<CategoryDetail> GetEntityById(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
