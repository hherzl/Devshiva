using System;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksAPI.Core.EntityLayer;

namespace AdventureWorksAPI.Core.DataLayer
{
    public interface IAdventureWorksRepository : IDisposable
    {
        IQueryable<Product> GetProducts(Int32 pageSize, Int32 pageNumber, String name);

        Task<Product> GetProductAsync(Product entity);

        Task<Product> AddProductAsync(Product entity);

        Task<Product> UpdateProductAsync(Product changes);

        Task<Product> DeleteProductAsync(Product changes);
    }
}
