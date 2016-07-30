using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureWorksAPI.Models
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

        public IEnumerable<Product> GetProducts(Int32 pageSize, Int32 pageNumber, String name)
        {
            var query = DbContext.Set<Product>().AsQueryable().Skip((pageNumber - 1) * pageSize).Take(pageSize);
            
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(item => item.Name.ToLower().Contains(name.ToLower()));
            }

            return query;
        }

        public Product GetProduct(Int32? id)
        {
            return DbContext.Set<Product>().FirstOrDefault(item => item.ProductID == id);
        }

        public Product AddProduct(Product entity)
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

            DbContext.SaveChanges();

            return entity;
        }

        public Product UpdateProduct(Int32? id, Product changes)
        {
            var entity = GetProduct(id);

            if (entity != null)
            {
                entity.Name = changes.Name;
                entity.ProductNumber = changes.ProductNumber;

                DbContext.SaveChanges();
            }

            return entity;
        }

        public Product DeleteProduct(Int32? id)
        {
            var entity = GetProduct(id);

            if (entity != null)
            {
                DbContext.Set<Product>().Remove(entity);

                DbContext.SaveChanges();
            }

            return entity;
        }
    }
}
