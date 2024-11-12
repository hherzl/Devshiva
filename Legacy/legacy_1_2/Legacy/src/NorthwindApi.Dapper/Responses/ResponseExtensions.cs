using System;
using Microsoft.Extensions.Logging;
using NorthwindApi.Exceptions;

namespace NorthwindApi.Responses
{
    public static class ResponseExtensions
    {
        public static void SetError(this IResponse response, ILogger logger, Exception ex)
        {
            response.DidError = true;

            var cast = ex as NorthwindException;

            if (cast == null)
            {
                response.ErrorMessage = "There was an internal error, please contact to administrator.";

                logger?.LogCritical(ex.ToString());
            }
            else
            {
                response.ErrorMessage = ex.Message;

                logger?.LogError(ex.ToString());
            }
        }
    }
}
