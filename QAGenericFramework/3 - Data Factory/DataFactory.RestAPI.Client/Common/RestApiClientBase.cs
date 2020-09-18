using DataFactory.RestAPI.Client.Contracts.CustomHttpClient;
using DataFactory.RestAPI.Entities.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataFactory.RestAPI.Client.Common
{
    public class RestApiClientBase
    {
        protected readonly IConfigurationRoot ConfigurationRoot;
        protected readonly IHttpClientFactory HttpClientFactory;

        public RestApiClientBase(IConfigurationRoot configurationRoot, IHttpClientFactory httpClientFactory)
        {
            this.ConfigurationRoot = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));
            this.HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        protected async Task<ApiGenericResponse<T>> CreateApiGenericResponse<T>(HttpResponseMessage response) where T : class
        {
            var headers = CreateHeadersDictionary(response);
            var status = ((int)response.StatusCode).ToString();
            var responseData = await CreateResponseData(response, status, headers);
            var result = JsonSerializer.Deserialize<T>(responseData);

            return new ApiGenericResponse<T>(status, headers, result);
        }

        protected async Task<ApiResponse> CreateApiResponse(HttpResponseMessage response)
        {
            var headers = CreateHeadersDictionary(response);
            var status = ((int)response.StatusCode).ToString();

            var responseData = await CreateResponseData(response, status, headers);

            return new ApiResponse(status, headers, responseData);
        }

        private async Task<string> CreateResponseData(HttpResponseMessage response, string status, IDictionary<string, IEnumerable<string>> headers)
        {
            var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            switch (status)
            {
                case "200":
                case "201":
                case "204":
                    return responseData;

                default:
                    ThrowApiException(status, responseData, headers);
                    return null;
            }
        }

        private IDictionary<string, IEnumerable<string>> CreateHeadersDictionary(HttpResponseMessage response)
        {
            var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);

            foreach (var item in response.Content.Headers)
            {
                headers[item.Key] = item.Value;
            }

            return headers;
        }

        private void ThrowApiException(string statusCode, string responseData, IDictionary<string, IEnumerable<string>> headers)
        {
            var message = $"Status code - {statusCode} - {responseData}";

            switch (statusCode)
            {
                case "400":
                    throw new ApiException($"Bad Request - {message}", statusCode, responseData, headers, null);

                case "404":
                    throw new ApiException($"Not Found - {message}", statusCode, responseData, headers, null);

                case "409":
                    throw new ApiException($"Conflict - {message}", statusCode, responseData, headers, null);

                case "413":
                case "422":
                    throw new ApiException($"Client Error - {message}", statusCode, responseData, headers, null);

                case "500":
                    throw new ApiException($"Server Error - {message}", statusCode, responseData, headers, null);

                case "502":
                    throw new ApiException($"Issue description - {message}", statusCode, responseData, headers, null);

                default:
                    throw new ApiException($"The HTTP status code of the response was not expected ({statusCode})", statusCode, responseData, headers, null);
            }
        }
    }
}