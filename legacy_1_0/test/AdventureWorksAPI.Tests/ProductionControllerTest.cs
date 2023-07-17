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
        public async Task TestGetProductsAsync()
        {
            // Arrange
            var repository = RepositoryMocker.GetAdventureWorksRepository();
            var controller = new ProductionController(repository);

            // Act
            var response = await controller.GetProductsAsync() as ObjectResult;
            var value = response.Value as IListModelResponse<ProductViewModel>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetProductAsync()
        {
            // Arrange
            var repository = RepositoryMocker.GetAdventureWorksRepository();
            var controller = new ProductionController(repository);
            var id = 1;

            // Act
            var response = await controller.GetProductAsync(id) as ObjectResult;
            var value = response.Value as ISingleModelResponse<ProductViewModel>;

            repository.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetNonExistingProductAsync()
        {
            // Arrange
            var repository = RepositoryMocker.GetAdventureWorksRepository();
            var controller = new ProductionController(repository);
            var id = 0;

            // Act
            var response = await controller.GetProductAsync(id) as ObjectResult;
            var value = response.Value as ISingleModelResponse<ProductViewModel>;

            repository.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestPostProductAsync()
        {
            // Arrange
            var repository = RepositoryMocker.GetAdventureWorksRepository();
            var controller = new ProductionController(repository);
            var request = new ProductViewModel
            {
                ProductName = String.Format("New test product {0}{1}{2}", DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond),
                ProductNumber = String.Format("{0}{1}{2}", DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond)
            };

            // Act
            var response = await controller.PostProductAsync(request) as ObjectResult;
            var value = response.Value as ISingleModelResponse<ProductViewModel>;

            repository.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestPutProductAsync()
        {
            // Arrange
            var repository = RepositoryMocker.GetAdventureWorksRepository();
            var controller = new ProductionController(repository);
            var id = 1;
            var request = new ProductViewModel
            {
                ProductID = id,
                ProductName = "New product test II",
                ProductNumber = "XYZ"
            };

            // Act
            var response = await controller.PutProductAsync(id, request) as ObjectResult;
            var value = response.Value as ISingleModelResponse<ProductViewModel>;

            repository.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestDeleteProductAsync()
        {
            // Arrange
            var repository = RepositoryMocker.GetAdventureWorksRepository();
            var controller = new ProductionController(repository);
            var id = 1000;

            // Act
            var response = await controller.DeleteProductAsync(id) as ObjectResult;
            var value = response.Value as ISingleModelResponse<ProductViewModel>;

            repository.Dispose();

            // Assert
            Assert.False(value.DidError);
        }
    }
}
