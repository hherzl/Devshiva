using System;
using System.Collections.Generic;

namespace AdventureWorksAPI.Models
{
    public interface IAdventureWorksRepository : IDisposable
    {
        IEnumerable<Product> GetProducts();

        Product GetProduct(Int32? id);
    }
}
