using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DataFactory.RestAPI.Client.CustomHttpClient
{
    public abstract class BaseRestsharpHttpClient
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly RestClient httpClient;

        public BaseRestsharpHttpClient(ITestOutputHelper testOutputHelper, HttpClientType httpClientType, string token)
        {
            this.testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));

            switch (httpClientType)
            {
                case HttpClientType.StandardHttpClient:
                    var authentication = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
                    var options = new RestClientOptions() { Authenticator = authentication };

                    httpClient = new RestClient(options);
                    break;

                default:
                    throw new HttpRequestException($"Http client type {httpClientType} not defined");
            }
        }

        public async Task<RestResponse<T>> GetAsync<T>(string url)
        {
            testOutputHelper.WriteLine($"Get call to {url}");

            var request = new RestRequest(url);

            var response = await httpClient.ExecuteGetAsync<T>(request);
            return response;
        }

        public async Task<RestResponse> PostAsync<T>(string url, T item)
        {
            testOutputHelper.WriteLine($"Post call to {url}");

            var jsonItem = JsonSerializer.Serialize(item);
            var request = new RestRequest(url).AddJsonBody(jsonItem);

            var response = await httpClient.PostAsync(request);
            return response;
        }

        public async Task<RestResponse> PutAsync<T>(string url, T item)
        {
            testOutputHelper.WriteLine($"Put call to {url}");

            var jsonItem = JsonSerializer.Serialize(item);
            var request = new RestRequest(url).AddJsonBody(jsonItem);

            var response = await httpClient.PutAsync(request);
            return response;
        }

        public async Task<RestResponse> DeleteAsync<T>(string url)
        {
            testOutputHelper.WriteLine($"Delete call to {url}");

            var request = new RestRequest(url);

            var response = await httpClient.DeleteAsync(request);
            return response;
        }
    }
}
