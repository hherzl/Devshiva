using System.Collections.Generic;

namespace NorthwindApi.Responses
{
    public class PagedListResponse<TModel> : IPagedListResponse<TModel> where TModel : class
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int ItemsCount { get; set; }

        public int PageCount
            => ItemsCount == 0 ? 0 : ItemsCount / PageSize;
    }
}
