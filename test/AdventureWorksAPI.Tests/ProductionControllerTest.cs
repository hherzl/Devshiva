using System;
using System.Threading.Tasks;
using AdventureWorksAPI.Controllers;
using AdventureWorksAPI.Responses;
using AdventureWorksAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AdventureWorksAPI.Tests
{
    public class ProductionControllerTest
    {
        [Fact]
        public async Task Production_GetProductsAsync()
        {
            using (var repository = RepositoryMocker.AdventureWorksRepository)
            {
                // Arrange
                var controller = new ProductionController(repository);

                // Act
                var response = await controller.GetProducts() as ObjectResult;

                // Assert
                var value = response.Value as IListModelResponse<ProductViewModel>;

                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task Production_GetProductAsync()
        {
            using (var repository = RepositoryMocker.AdventureWorksRepository)
            {
                // Arrange
                var controller = new ProductionController(repository);
                var id = 1;

                // Act
                var response = await controller.GetProduct(id) as ObjectResult;

                // Assert
                var value = response.Value as ISingleModelResponse<ProductViewModel>;

                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task Production_GetNonExistingProductAsync()
        {
            using (var repository = RepositoryMocker.AdventureWorksRepository)
            {
                // Arrange
                var controller = new ProductionController(repository);
                var id = 0;

                // Act
                var response = await controller.GetProduct(id) as ObjectResult;

                // Assert
                var value = response.Value as ISingleModelResponse<ProductViewModel>;

                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task Production_CreateProductAsync()
        {
            using (var repository = RepositoryMocker.AdventureWorksRepository)
            {
                // Arrange
                var controller = new ProductionController(repository);

                var viewModel = new ProductViewModel
                {
                    ProductName = String.Format("New test product {0}{1}{2}", DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond),
                    ProductNumber = String.Format("{0}{1}{2}", DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond)
                };

                // Act
                var response = await controller.CreateProduct(viewModel) as ObjectResult;

                // Assert
                var value = response.Value as ISingleModelResponse<ProductViewModel>;

                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task Production_UpdateProductAsync()
        {
            using (var repository = RepositoryMocker.AdventureWorksRepository)
            {
                // Arrange
                var controller = new ProductionController(repository);

                var id = 1;

                var viewModel = new ProductViewModel
                {
                    ProductName = "New product test II",
                    ProductNumber = "XYZ"
                };

                // Act
                var response = await controller.UpdateProduct(id, viewModel) as ObjectResult;

                // Assert
                var value = response.Value as ISingleModelResponse<ProductViewModel>;

                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task Production_DeleteProductAsync()
        {
            using (var repository = RepositoryMocker.AdventureWorksRepository)
            {
                // Arrange
                var controller = new ProductionController(repository);
                var id = 1000;

                // Act
                var response = await controller.DeleteProduct(id) as ObjectResult;

                // Assert
                var value = response.Value as ISingleModelResponse<ProductViewModel>;

                Assert.False(value.DidError);
            }
        }
    }
}
