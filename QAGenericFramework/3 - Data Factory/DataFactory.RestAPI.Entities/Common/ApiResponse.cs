using System.Collections.Generic;

namespace DataFactory.RestAPI.Entities.Common
{
    public class ApiResponse
    {
        public ApiResponse(string statusCode, IDictionary<string, IEnumerable<string>> headers)
        {
            StatusCode = statusCode;
            Headers = headers;
        }

        public ApiResponse(string statusCode, IDictionary<string, IEnumerable<string>> headers, byte[] data)
            : this(statusCode, headers)
        {
            Data = data;
        }

        public ApiResponse(string statusCode, IDictionary<string, IEnumerable<string>> headers, string body)
            : this(statusCode, headers)
        {
            Body = body;
        }

        public string StatusCode { get; private set; }

        public byte[] Data { get; private set; }

        public IDictionary<string, IEnumerable<string>> Headers { get; private set; }

        public string Body { get; set; }
    }
}