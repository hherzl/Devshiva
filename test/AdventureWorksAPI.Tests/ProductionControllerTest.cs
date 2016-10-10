using System;
using System.Threading.Tasks;
using AdventureWorksAPI.Controllers;
using AdventureWorksAPI.Models;
using AdventureWorksAPI.Responses;
using AdventureWorksAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AdventureWorksAPI.Tests
{
    public class ProductionControllerTest
    {
        String AdventureWorksConnectionString
        {
            get
            {
                return "server=(local);database=AdventureWorks2012;integrated security=yes;";
            }
        }

        IAdventureWorksRepository AdventureWorksRepository
        {
            get
            {
                return new AdventureWorksRepository(new AdventureWorksDbContext(AdventureWorksConnectionString)) as IAdventureWorksRepository;
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
                ProductNumber = "024688"
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
                ProductNumber = "33669922"
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
