using System;

namespace NorthwindApi.Exceptions
{
    public class NorthwindException : Exception
    {
        public NorthwindException()
            : base()
        {
        }

        public NorthwindException(string message)
            : base(message)
        {
        }

        public NorthwindException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
