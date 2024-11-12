using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected AppSettings AppSettings;

        public WarehouseController(ILogger<WarehouseController> logger, IOptions<AppSettings> options)
        {
            Logger = logger;
            AppSettings = options.Value;
        }

        // GET
        // api/v1/Warehouse/StockItem

        [HttpGet("StockItem")]
        public async Task<IActionResult> GetStockItemsAsync(int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemsAsync));

            var response = new PagedResponse<StockItem>();

            try
            {
                using (var connection = AppSettings.CreateConnection())
                {
                    response.ItemsCount = await connection.CountStockItemsAsync(lastEditedBy, colorID, outerPackageID, supplierID);

                    response.Model = await connection.GetStockItemsAsync(pageSize, pageNumber, lastEditedBy, colorID, outerPackageID, supplierID);

                    Logger?.LogInformation("The stock items have been retrieved successfully.");
                }
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

        [HttpGet("StockItem/{id}")]
        public async Task<IActionResult> GetStockItemAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemAsync));

            var response = new SingleResponse<StockItem>();

            try
            {
                using (var connection = AppSettings.CreateConnection())
                {
                    response.Model = await connection.GetStockItemAsync(new StockItem(id));

                    Logger?.LogInformation("The stock item have been retrieved successfully.");
                }
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}
