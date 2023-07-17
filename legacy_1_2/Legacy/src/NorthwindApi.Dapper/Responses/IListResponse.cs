using System.Collections.Generic;

namespace NorthwindApi.Responses
{
    public interface IListResponse<TModel> : IResponse where TModel : class
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
