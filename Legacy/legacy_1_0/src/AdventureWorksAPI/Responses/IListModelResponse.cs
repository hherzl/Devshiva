using System;
using System.Collections.Generic;

namespace AdventureWorksAPI.Responses
{
    public interface IListModelResponse<TModel> : IResponse
    {
        Int32 PageSize { get; set; }

        Int32 PageNumber { get; set; }

        IEnumerable<TModel> Model { get; set; }
    }
}
