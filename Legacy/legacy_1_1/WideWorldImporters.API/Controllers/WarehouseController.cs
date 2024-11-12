using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API.Controllers
{
#pragma warning disable CS1591
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly WideWorldImportersDbContext DbContext;

        public WarehouseController(ILogger<WarehouseController> logger, WideWorldImportersDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }
#pragma warning restore CS1591

        // GET
        // api/v1/Warehouse/StockItem

        /// <summary>
        /// Retrieves stock items
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="lastEditedBy">Last edit by (user id)</param>
        /// <param name="colorID">Color id</param>
        /// <param name="outerPackageID">Outer package id</param>
        /// <param name="supplierID">Supplier id</param>
        /// <param name="unitPackageID">Unit package id</param>
        /// <returns>A response with stock items list</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("StockItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStockItemsAsync(int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemsAsync));

            var response = new PagedResponse<StockItem>();

            try
            {
                // Get the "proposed" query from repository
                var query = DbContext.GetStockItems();

                // Set paging values
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;

                // Get the total rows
                response.ItemsCount = await query.CountAsync();

                // Get the specific page from database
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

                response.Message = string.Format("Page {0} of {1}, Total of products: {2}.", pageNumber, response.PageCount, response.ItemsCount);

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
        // api/v1/Warehouse/StockItem/5

        /// <summary>
        /// Retrieves a stock item by ID
        /// </summary>
        /// <param name="id">Stock item id</param>
        /// <returns>A response with stock item</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="404">If stock item is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("StockItem/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStockItemAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemAsync));

            var response = new SingleResponse<StockItem>();

            try
            {
                // Get the stock item by id
                response.Model = await DbContext.GetStockItemsAsync(new StockItem(id));
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
        // api/v1/Warehouse/StockItem/

        /// <summary>
        /// Creates a new stock item
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new stock item</returns>
        /// <response code="201">A response as creation of stock item</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("StockItem")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostStockItemAsync([FromBody]PostStockItemsRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostStockItemAsync));

            var response = new SingleResponse<StockItem>();

            try
            {
                var existingEntity = await DbContext
                    .GetStockItemsByStockItemNameAsync(new StockItem { StockItemName = request.StockItemName });

                if (existingEntity != null)
                    ModelState.AddModelError("StockItemName", "Stock item name already exists");

                if (!ModelState.IsValid)
                    return BadRequest();

                // Create entity from request model
                var entity = request.ToEntity();

                // Add entity to repository
                DbContext.Add(entity);

                // Save entity in database
                await DbContext.SaveChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostStockItemAsync), ex);
            }

            return response.ToHttpCreatedResponse();
        }

        // PUT
        // api/v1/Warehouse/StockItem/5

        /// <summary>
        /// Updates an existing stock item
        /// </summary>
        /// <param name="id">Stock item ID</param>
        /// <param name="request">Request model</param>
        /// <returns>A response as update stock item result</returns>
        /// <response code="200">If stock item was updated successfully</response>
        /// <response code="400">For bad request</response>
        /// <response code="404">If stock item is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("StockItem/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutStockItemAsync(int id, [FromBody]PutStockItemsRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PutStockItemAsync));

            var response = new Response();

            try
            {
                // Get stock item by id
                var entity = await DbContext.GetStockItemsAsync(new StockItem(id));

                // Validate if entity exists
                if (entity == null)
                    return NotFound();

                // Set changes to entity
                entity.StockItemName = request.StockItemName;
                entity.SupplierID = request.SupplierID;
                entity.ColorID = request.ColorID;
                entity.UnitPrice = request.UnitPrice;

                // Update entity in repository
                DbContext.Update(entity);

                // Save entity in database
                await DbContext.SaveChangesAsync();
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
        // api/v1/Warehouse/StockItem/5

        /// <summary>
        /// Deletes an existing stock item
        /// </summary>
        /// <param name="id">Stock item ID</param>
        /// <returns>A response as delete stock item result</returns>
        /// <response code="200">If stock item was deleted successfully</response>
        /// <response code="404">If stock item is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("StockItem/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteStockItemAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteStockItemAsync));

            var response = new Response();

            try
            {
                // Get stock item by id
                var entity = await DbContext.GetStockItemsAsync(new StockItem(id));

                // Validate if entity exists
                if (entity == null)
                    return NotFound();

                // Remove entity from repository
                DbContext.Remove(entity);

                // Delete entity in database
                await DbContext.SaveChangesAsync();
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
