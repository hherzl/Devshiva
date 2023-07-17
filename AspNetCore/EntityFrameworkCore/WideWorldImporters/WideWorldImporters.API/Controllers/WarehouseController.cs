using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API.Controllers;

[ApiController]
[Route("api/v1")]
public class WarehouseController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly WideWorldImportersDbContext _dbContext;

    public WarehouseController(ILogger<WarehouseController> logger, WideWorldImportersDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("stock-item")]
    [ProducesResponseType(200, Type = typeof(PagedResponse<StockItem>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetStockItemsAsync([FromQuery] SearchStockItemsParameters request)
    {
        _logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemsAsync));

        var response = new PagedResponse<StockItem>();

        // Get the "proposed" query from repository
        var query = _dbContext.GetStockItems(request);

        // Set paging values
        response.PageSize = request.PageSize;
        response.PageNumber = request.PageNumber;

        // Get the total rows
        response.ItemsCount = await query.CountAsync();

        // Get the specific page from database
        response.Model = await query
            .Paging(request.PageSize, request.PageNumber)
            .ToListAsync()
            ;

        response.Message = string.Format("Page {0} of {1}, Total of stock items: {2}.", request.PageNumber, response.PageCount, response.ItemsCount);

        _logger?.LogInformation("The stock items have been retrieved successfully.");

        return response.ToOkResult();
    }

    [HttpGet("stock-item/{id}")]
    [ProducesResponseType(200, Type = typeof(SingleResponse<StockItemDetailsModel>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetStockItemAsync(int id)
    {
        _logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemAsync));

        // Get the stock item by id
        var entity = await _dbContext.GetStockItemsAsync(new StockItem(id));

        if (entity == null)
            return NotFound();

        var model = new StockItemDetailsModel
        {
            StockItemID = entity.StockItemID,
            StockItemName = entity.StockItemName,
            SupplierID = entity.SupplierID,
            UnitPrice = entity.UnitPrice
        };

        var response = new SingleResponse<StockItemDetailsModel>(model);

        return response.ToOkResult();
    }

    [HttpPost("stock-item")]
    [ProducesResponseType(201, Type = typeof(CreatedResponse<int?>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostStockItemAsync([FromBody] PostStockItemsRequest request)
    {
        _logger?.LogDebug("'{0}' has been invoked", nameof(PostStockItemAsync));

        var existingEntity = await _dbContext
            .GetStockItemsByStockItemNameAsync(new StockItem { StockItemName = request.StockItemName });

        if (existingEntity != null)
            ModelState.AddModelError("StockItemName", "Stock item name already exists");

        if (!ModelState.IsValid)
            return BadRequest();

        // Create entity from request model
        var entity = request.ToEntity();

        _dbContext.Add(entity);

        // Save entity in database
        await _dbContext.SaveChangesAsync();

        var response = new CreatedResponse<int?>(entity.StockItemID);

        return response.ToCreatedResult();
    }

    [HttpPut("stock-item/{id}")]
    [ProducesResponseType(200, Type = typeof(Response))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PutStockItemAsync(int id, [FromBody] PutStockItemsRequest request)
    {
        _logger?.LogDebug("'{0}' has been invoked", nameof(PutStockItemAsync));

        var response = new Response();

        // Get stock item by id
        var entity = await _dbContext.GetStockItemsAsync(new StockItem(id));

        // Validate if entity exists
        if (entity == null)
            return NotFound();

        // Set changes to entity
        entity.StockItemName = request.StockItemName;
        entity.SupplierID = request.SupplierID;
        entity.UnitPrice = request.UnitPrice;

        _dbContext.Update(entity);

        // Save changes in database
        await _dbContext.SaveChangesAsync();

        return response.ToOkResult();
    }

    [HttpDelete("stock-item/{id}")]
    [ProducesResponseType(200, Type = typeof(Response))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteStockItemAsync(int id)
    {
        _logger?.LogDebug("'{0}' has been invoked", nameof(DeleteStockItemAsync));

        // Get stock item by id
        var entity = await _dbContext.GetStockItemsAsync(new StockItem(id));

        // Validate if entity exists
        if (entity == null)
            return NotFound();

        _dbContext.Remove(entity);

        // Delete entity in database
        await _dbContext.SaveChangesAsync();

        var response = new Response();

        return response.ToOkResult();
    }
}
