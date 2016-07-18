using AdventureWorksAPI.Models;
using AdventureWorksAPI.ViewModels;

namespace AdventureWorksAPI.Extensions
{
    public static class ProductViewModelMapper
    {
        public static ProductViewModel ToViewModel(this Product entity)
        {
            return entity == null ? null : new ProductViewModel
            {
                ProductID = entity.ProductID,
                ProductName = entity.Name,
                ProductNumber = entity.ProductNumber
            };
        }
    }
}
