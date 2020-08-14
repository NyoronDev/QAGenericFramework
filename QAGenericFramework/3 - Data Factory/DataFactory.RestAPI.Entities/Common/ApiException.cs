using System;
using System.Collections.Generic;

namespace DataFactory.RestAPI.Entities.Common
{
    public class ApiException : Exception
    {
        public ApiException(string message, string statusCode, string response, IDictionary<string, IEnumerable<string>> headers, System.Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public string StatusCode { get; private set; }

        public string Response { get; private set; }

        public IDictionary<string, IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public override string ToString()
        {
            return $"HTTP Response: \n\n{Response}\n\n{base.ToString()}";
        }
    }
}