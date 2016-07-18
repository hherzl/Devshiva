using System;
using System.Collections.Generic;

namespace AdventureWorksAPI.Responses
{
    public class ListModelResponse<TModel> : IListModelResponse<TModel>
    {
        public ListModelResponse()
        {

        }

        public String Message { get; set; }

        public Boolean DidError { get; set; }

        public String ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }
    }
}
