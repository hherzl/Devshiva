using System.Collections.Generic;

namespace AdventureWorksAPI.Responses
{
    public interface IListModelResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
