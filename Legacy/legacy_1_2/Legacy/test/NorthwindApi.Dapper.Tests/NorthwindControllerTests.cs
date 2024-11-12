using System;
using System.Linq;
using System.Threading.Tasks;
using NorthwindApi.Dapper.Controllers;
using NorthwindApi.Dapper.Models;
using Xunit;

namespace NorthwindApi.Dapper.Tests
{
    public class NorthwindControllerTests
    {
        [Fact]
        public async Task TestGetProductsAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<NorthwindController>();
            var repository = RepositoryMocker.GetNorthwindRepository();
            var controller = new NorthwindController(logger, repository);

            // Act
            var response = await controller.GetProductsAsync();

            // Asert
            Assert.False(response.DidError);
            Assert.True(response.Model.Count() > 0);
        }

        [Fact]
        public async Task TestGetProductsBySupplierAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<NorthwindController>();
            var repository = RepositoryMocker.GetNorthwindRepository();
            var controller = new NorthwindController(logger, repository);
            var supplierID = 1;

            // Act
            var response = await controller.GetProductsAsync(supplierID: supplierID);

            // Asert
            Assert.False(response.DidError);
            Assert.True(response.Model.Count() > 0);
            Assert.True(response.Model.Where(item => item.SupplierID == supplierID).Count() == response.Model.Count());
        }

        [Fact]
        public async Task TestGetProductsByCategoryAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<NorthwindController>();
            var repository = RepositoryMocker.GetNorthwindRepository();
            var controller = new NorthwindController(logger, repository);
            var categoryID = 1;

            // Act
            var response = await controller.GetProductsAsync(categoryID: categoryID);

            // Asert
            Assert.False(response.DidError);
            Assert.True(response.Model.Count() > 0);
            Assert.True(response.Model.Where(item => item.CategoryID == categoryID).Count() == response.Model.Count());
        }

        [Fact]
        public async Task TestGetProductAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<NorthwindController>();
            var repository = RepositoryMocker.GetNorthwindRepository();
            var controller = new NorthwindController(logger, repository);
            var request = 10;

            // Act
            var response = await controller.GetProductAsync(request);

            // Asert
            Assert.False(response.DidError);
            Assert.True(response.Model != null);
        }

        [Fact]
        public async Task TestGetNonExistingProductAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<NorthwindController>();
            var repository = RepositoryMocker.GetNorthwindRepository();
            var controller = new NorthwindController(logger, repository);
            var request = 0;

            // Act
            var response = await controller.GetProductAsync(request);

            // Asert
            Assert.False(response.DidError);
            Assert.True(response.Model == null);
        }

        [Fact]
        public async Task TestPostProductAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<NorthwindController>();
            var repository = RepositoryMocker.GetNorthwindRepository();
            var controller = new NorthwindController(logger, repository);
            var request = new Product
            {
                ProductName = string.Format("Product test {0}{1}{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                SupplierID = 1,
                CategoryID = 1,
                Discontinued = false
            };

            // Act
            var response = await controller.PostProductAsync(request);

            // Asert
            Assert.False(response.DidError);
            Assert.True(response.Model.ProductID.HasValue);
        }

        [Fact]
        public async Task TestPutProductAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<NorthwindController>();
            var repository = RepositoryMocker.GetNorthwindRepository();
            var controller = new NorthwindController(logger, repository);
            var id = 85;
            var request = new Product
            {
                ProductID = 85,
                ProductName = string.Format("Product test {0}{1}{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                SupplierID = 2,
                CategoryID = 3
            };

            // Act
            var response = await controller.PutProductAsync(id, request);

            // Asert
            Assert.False(response.DidError);
        }

        [Fact]
        public async Task TestDeleteProductAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<NorthwindController>();
            var repository = RepositoryMocker.GetNorthwindRepository();
            var controller = new NorthwindController(logger, repository);
            var id = 85;

            // Act
            var response = await controller.DeleteProductAsync(id);

            // Asert
            Assert.False(response.DidError);
        }
    }
}
