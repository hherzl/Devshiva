using System;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksAPI.Core.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksAPI.Core.DataLayer
{
    public class AdventureWorksRepository : IAdventureWorksRepository
    {
        private readonly AdventureWorksDbContext DbContext;
        private Boolean Disposed;

        public AdventureWorksRepository(AdventureWorksDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();

                    Disposed = true;
                }
            }
        }

        public IQueryable<Product> GetProducts(Int32 pageSize, Int32 pageNumber, String name)
        {
            var query = DbContext.Set<Product>().Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(item => item.Name.ToLower().Contains(name.ToLower()));
            }

            return query;
        }

        public Task<Product> GetProductAsync(Product entity)
        {
            return DbContext.Set<Product>().FirstOrDefaultAsync(item => item.ProductID == entity.ProductID);
        }

        public async Task<Product> AddProductAsync(Product entity)
        {
            entity.MakeFlag = false;
            entity.FinishedGoodsFlag = false;
            entity.SafetyStockLevel = 1;
            entity.ReorderPoint = 1;
            entity.StandardCost = 0.0m;
            entity.ListPrice = 0.0m;
            entity.DaysToManufacture = 0;
            entity.SellStartDate = DateTime.Now;
            entity.rowguid = Guid.NewGuid();
            entity.ModifiedDate = DateTime.Now;

            DbContext.Set<Product>().Add(entity);

            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Product> UpdateProductAsync(Product changes)
        {
            var entity = await GetProductAsync(changes);

            if (entity != null)
            {
                entity.Name = changes.Name;
                entity.ProductNumber = changes.ProductNumber;

                await DbContext.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<Product> DeleteProductAsync(Product changes)
        {
            var entity = await GetProductAsync(changes);

            if (entity != null)
            {
                DbContext.Set<Product>().Remove(entity);

                await DbContext.SaveChangesAsync();
            }

            return entity;
        }
    }
}
