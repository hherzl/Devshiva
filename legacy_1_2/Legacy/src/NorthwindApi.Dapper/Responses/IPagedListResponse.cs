namespace NorthwindApi.Responses
{
    public interface IPagedListResponse<TModel> : IListResponse<TModel> where TModel : class
    {
        int PageSize { get; set; }

        int PageNumber { get; set; }

        int ItemsCount { get; set; }

        int PageCount { get; }
    }
}
