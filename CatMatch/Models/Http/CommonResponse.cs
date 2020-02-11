using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Http.Models
{
    public struct CommonResponse<T> where T : class
    {
        public int InternalCode { get; private set; }
        public string Message { get; private set; }
        public T Payload { get; private set; }

        public CommonResponse(int internalCode, string message, T payload)
        {
            InternalCode = internalCode;
            Message = message;
            Payload = payload;
        }
    }
}
