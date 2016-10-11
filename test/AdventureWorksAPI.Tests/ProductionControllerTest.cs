using System;
using System.Threading.Tasks;
using AdventureWorksAPI.Controllers;
using AdventureWorksAPI.Models;
using AdventureWorksAPI.Responses;
using AdventureWorksAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Xunit;

namespace AdventureWorksAPI.Tests
{
    public class ProductionControllerTest
    {
        private IAdventureWorksRepository AdventureWorksRepository
        {
            get
            {
                var appSettings = Options.Create(new AppSettings
                {
                    ConnectionString = "server=(local);database=AdventureWorks2012;integrated security=yes;"
                });

                return new AdventureWorksRepository(new AdventureWorksDbContext(appSettings)) as IAdventureWorksRepository;
            }
        }

        [Fact]
        public async Task Production_GetProductsAsync()
        {
            // Arrange
            var controller = new ProductionController(AdventureWorksRepository);

            // Act
            var response = await controller.GetProducts() as ObjectResult;

            // Assert
            var value = response.Value as IListModelResponse<ProductViewModel>;

            Assert.False(value.DidError);
        }

        [Fact]
        public async Task Production_GetProductAsync()
        {
            // Arrange
            var controller = new ProductionController(AdventureWorksRepository);

            // Act
            var response = await controller.GetProduct(1) as ObjectResult;

            // Assert
            var value = response.Value as ISingleModelResponse<ProductViewModel>;

            Assert.False(value.DidError);
        }

        [Fact]
        public async Task Production_CreateProductAsync()
        {
            // Arrange
            var controller = new ProductionController(AdventureWorksRepository);

            var viewModel = new ProductViewModel
            {
                ProductName = "New product",
                ProductNumber = "98765"
            };

            // Act
            var response = await controller.CreateProduct(viewModel) as ObjectResult;

            // Assert
            var value = response.Value as ISingleModelResponse<ProductViewModel>;

            Console.WriteLine("error: {0}", value.ErrorMessage);

            Assert.False(value.DidError);
        }

        [Fact]
        public async Task Production_UpdateProductAsync()
        {
            // Arrange
            var controller = new ProductionController(AdventureWorksRepository);

            var id = 1;

            var viewModel = new ProductViewModel
            {
                ProductName = "New name for product",
                ProductNumber = "12345"
            };

            // Act
            var response = await controller.UpdateProduct(id, viewModel) as ObjectResult;

            // Assert
            var value = response.Value as ISingleModelResponse<ProductViewModel>;

            Assert.False(value.DidError);
        }

        [Fact]
        public async Task Production_DeleteProductAsync()
        {
            // Arrange
            var controller = new ProductionController(AdventureWorksRepository);

            var id = 1000;

            // Act
            var response = await controller.DeleteProduct(id) as ObjectResult;

            // Assert
            var value = response.Value as ISingleModelResponse<ProductViewModel>;

            Assert.False(value.DidError);
        }
    }
}
