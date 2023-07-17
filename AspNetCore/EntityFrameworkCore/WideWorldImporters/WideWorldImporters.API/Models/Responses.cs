using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WideWorldImporters.API.Models;

public record Response
{
    public string Message { get; set; }
}

public record SingleResponse<TModel> : Response
{
    public SingleResponse()
    {
    }

    public SingleResponse(TModel model)
    {
        Model = model;
    }

    public TModel Model { get; set; }
}

public record ListResponse<TModel> : Response
{
    public IEnumerable<TModel> Model { get; set; }
}

public record PagedResponse<TModel> : ListResponse<TModel>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int ItemsCount { get; set; }

    public double PageCount
        => ItemsCount < PageSize ? 1 : (int)(((double)ItemsCount / PageSize) + 1);
}

public record CreatedResponse<TKey>
{
    public CreatedResponse()
    {
    }

    public CreatedResponse(TKey id)
    {
        Id = id;
    }

    public TKey Id { get; set; }
}

public static class ResponseExtensions
{
    public static IActionResult ToOkResult(this Response response)
        => new ObjectResult(response)
        {
            StatusCode = (int)HttpStatusCode.OK
        };

    public static IActionResult ToCreatedResult<TKey>(this CreatedResponse<TKey> response)
        => new ObjectResult(response)
        {
            StatusCode = (int)HttpStatusCode.Created
        };
}

public record StockItemDetailsModel
{
    public int? StockItemID { get; set; }
    public string StockItemName { get; set; }
    public int? SupplierID { get; set; }
    public decimal? UnitPrice { get; set; }
}
