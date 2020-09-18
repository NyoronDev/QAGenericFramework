using System.Collections.Generic;

namespace DataFactory.RestAPI.Entities.Common
{
    public class ApiGenericResponse<TResult> : ApiResponse
    {
        public TResult Result { get; private set; }

        public ApiGenericResponse(string statusCode, IDictionary<string, IEnumerable<string>> headers, TResult result)
            : base(statusCode, headers)
        {
            Result = result;
        }

        public ApiGenericResponse(string statusCode, IDictionary<string, IEnumerable<string>> headers)
            : base(statusCode, headers)
        {
        }

        public ApiGenericResponse(string statusCode, IDictionary<string, IEnumerable<string>> headers, string body)
            : base(statusCode, headers, body)
        {
        }
    }
}