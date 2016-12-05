using System;
using System.Collections.Generic;
using AdventureWorksAPI.Core.EntityLayer;

namespace AdventureWorksAPI.Core.DataLayer
{
    public interface IAdventureWorksRepository : IDisposable
    {
        IEnumerable<Product> GetProducts(Int32 pageSize, Int32 pageNumber, String name);

        Product GetProduct(Int32? id);

        Product AddProduct(Product entity);

        Product UpdateProduct(Int32? id, Product changes);

        Product DeleteProduct(Int32? id);
    }
}
