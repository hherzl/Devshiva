using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindApi.Dapper.Models;
using NorthwindApi.Responses;

namespace NorthwindApi.Dapper.Controllers
{
    [Route("api/[controller]")]
    public class NorthwindController : Controller
    {
        protected ILogger Logger;
        protected INorthwindRepository Repository;

        public NorthwindController(ILogger logger, INorthwindRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }

        // GET: api/Northwind/Product

        [HttpGet("Product")]
        public async Task<IListResponse<Product>> GetProductsAsync(int? supplierID = null, int? categoryID = null)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetProductsAsync));

            var response = new ListResponse<Product>();

            try
            {
                response.Model = (await Repository.GetProductsAsync(supplierID, categoryID)).ToList();
            }
            catch (Exception ex)
            {
                response.SetError(Logger, ex);
            }

            return response;
        }

        // GET: api/Northwind/Product

        [HttpGet("Product/{id}")]
        public async Task<ISingleResponse<Product>> GetProductAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetProductAsync));

            var response = new SingleResponse<Product>();

            try
            {
                response.Model = await Repository.GetProductAsync(new Product { ProductID = id });
            }
            catch (Exception ex)
            {
                response.SetError(Logger, ex);
            }

            return response;
        }

        // POST: api/Northwind/Product

        [HttpPost("Product")]
        public async Task<ISingleResponse<Product>> PostProductAsync([FromBody]Product request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostProductAsync));

            var response = new SingleResponse<Product>();

            try
            {
                await Repository.CreateProductAsync(request);

                response.Model = request;
            }
            catch (Exception ex)
            {
                response.SetError(Logger, ex);
            }

            return response;
        }

        // PUT: api/Northwind/Product

        [HttpPost("Product")]
        public async Task<ISingleResponse<Product>> PutProductAsync(int id, [FromBody]Product request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PutProductAsync));

            var response = new SingleResponse<Product>();

            try
            {
                var entity = await Repository.GetProductAsync(new Product { ProductID = id });

                if (entity != null)
                {
                    entity.ProductName = request.ProductName;
                    entity.SupplierID = request.SupplierID;
                    entity.CategoryID = request.CategoryID;
                    entity.UnitPrice = request.UnitPrice;

                    await Repository.UpdateProductAsync(request);

                    response.Model = request;
                }
            }
            catch (Exception ex)
            {
                response.SetError(Logger, ex);
            }

            return response;
        }

        // DELETE: api/Northwind/Product

        [HttpPost("Product")]
        public async Task<ISingleResponse<Product>> DeleteProductAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteProductAsync));

            var response = new SingleResponse<Product>();

            try
            {
                var entity = await Repository.GetProductAsync(new Product { ProductID = id });

                if (entity != null)
                {
                    await Repository.DeleteProductAsync(entity);

                    response.Model = entity;
                }
            }
            catch (Exception ex)
            {
                response.SetError(Logger, ex);
            }

            return response;
        }
    }
}
