using System;

namespace CatMatch.Models
{
    public class ServiceException : Exception
    {
        public int InternalCode { get; }

        public ServiceException(string message, int internalCode) : base(message)
        {
            InternalCode = internalCode;
        }
    }
}
