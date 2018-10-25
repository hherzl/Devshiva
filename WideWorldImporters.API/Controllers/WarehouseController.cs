using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly IWarehouseRepository Repository;

        public WarehouseController(ILogger<WarehouseController> logger, IWarehouseRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }

        // GET
        // api/v1/StockItem

        [HttpGet("StockItem")]
        public async Task<IActionResult> GetStockItemsAsync(int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemsAsync));

            var response = new PagedResponse<StockItem>();

            try
            {
                // Get the "proposed" query from repository
                var query = Repository.GetStockItems();

                // Set paging values
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;

                // Get the total rows
                response.ItemsCount = await query.CountAsync();

                // Get the specific page from database
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

                Logger?.LogInformation("The stock items have been retrieved successfully.");
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemsAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // GET
        // api/v1/StockItem/5

        [HttpGet("StockItem/{id}")]
        public async Task<IActionResult> GetStockItemAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemAsync));

            var response = new SingleResponse<StockItem>();

            try
            {
                // Get the stock item by id
                response.Model = await Repository.GetStockItemsAsync(new StockItem(id));
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // POST
        // api/v1/StockItem/

        [HttpPost("StockItem")]
        public async Task<IActionResult> PostStockItemAsync([FromBody]PostStockItemsRequestModel requestModel)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostStockItemAsync));

            var response = new SingleResponse<StockItem>();

            try
            {
                var existingEntity = await Repository
                    .GetStockItemsByStockItemNameAsync(new StockItem { StockItemName = requestModel.StockItemName });

                if (existingEntity != null)
                    ModelState.AddModelError("StockItemName", "Stock item name already exists");

                if (!ModelState.IsValid)
                    return BadRequest();

                // Create entity from request model
                var entity = requestModel.ToEntity();

                // Add entity to repository
                Repository.Add(entity);

                // Save entity in database
                await Repository.CommitChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // PUT
        // api/v1/StockItem/5

        [HttpPut("StockItem/{id}")]
        public async Task<IActionResult> PutStockItemAsync(int id, [FromBody]PutStockItemsRequestModel requestModel)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PutStockItemAsync));

            var response = new SingleResponse<StockItem>();

            try
            {
                // Get stock item by id
                var entity = await Repository.GetStockItemsAsync(new StockItem(id));

                // Validate if entity exists
                if (entity == null)
                    return response.ToHttpResponse();

                // Set changes to entity
                entity.StockItemName = requestModel.StockItemName;
                entity.SupplierID = requestModel.SupplierID;
                entity.ColorID = requestModel.ColorID;
                entity.UnitPrice = requestModel.UnitPrice;

                // Update entity in repository
                Repository.Update(entity);

                // Save entity in database
                await Repository.CommitChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // DELETE
        // api/v1/StockItem/5

        [HttpDelete("StockItem/{id}")]
        public async Task<IActionResult> DeleteStockItemAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteStockItemAsync));

            var response = new SingleResponse<StockItem>();

            try
            {
                // Get stock item by id
                var entity = await Repository.GetStockItemsAsync(new StockItem(id));

                // Validate if entity exists
                if (entity == null)
                    return response.ToHttpResponse();

                // Remove entity from repository
                Repository.Remove(entity);

                // Delete entity in database
                await Repository.CommitChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(DeleteStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}
