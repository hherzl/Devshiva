using System.Collections.Generic;

namespace NorthwindApi.Responses
{
    public class ListResponse<TModel> : IListResponse<TModel> where TModel : class
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }
    }
}
