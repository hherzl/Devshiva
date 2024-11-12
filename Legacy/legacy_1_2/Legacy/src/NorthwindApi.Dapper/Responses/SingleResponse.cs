namespace NorthwindApi.Responses
{
    public class SingleResponse<TModel> : ISingleResponse<TModel> where TModel : class
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public TModel Model { get; set; }
    }
}
