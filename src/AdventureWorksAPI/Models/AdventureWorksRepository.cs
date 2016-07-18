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

        public IEnumerable<Product> GetProducts()
        {
            return DbContext.Set<Product>();
        }

        public Product GetProduct(Int32? id)
        {
            return DbContext.Set<Product>().FirstOrDefault(item => item.ProductID == id);
        }
    }
}
