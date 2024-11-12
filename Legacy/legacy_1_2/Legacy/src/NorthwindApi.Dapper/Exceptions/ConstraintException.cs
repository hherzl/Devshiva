using System;

namespace NorthwindApi.Exceptions
{
    public class ConstraintException : NorthwindException
    {
        public ConstraintException()
            : base()
        {
        }

        public ConstraintException(string message)
            : base(message)
        {
        }

        public ConstraintException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
