using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DataFactory.RestAPI.Client.CustomHttpClient
{
    public abstract class BaseHttpClient
    {
        private readonly string jsonMediaType = "application/json";

        private readonly ITestOutputHelper testOutputHelper;
        private readonly HttpClient httpClient;

        public BaseHttpClient(ITestOutputHelper testOutputHelper, HttpClientType httpClientType)
        {
            this.testOutputHelper = testOutputHelper;

            var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true };

            switch (httpClientType)
            {
                case HttpClientType.StandardHttpClient:

                    httpClient = new HttpClient(handler);
                    break;

                default:
                    throw new HttpRequestException($"Http client type {httpClientType} not defined");
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string url, string authorizationToken = null)
        {
            testOutputHelper.WriteLine($"Get call to {url}");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(url, UriKind.RelativeOrAbsolute));
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            AddToken(requestMessage, authorizationToken);

            var response = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T item, string authorizationToken = null)
        {
            testOutputHelper.WriteLine($"Post call to {url}");

            var content = new StringContent(JsonSerializer.Serialize(item));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(jsonMediaType);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(url, UriKind.RelativeOrAbsolute));
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));
            requestMessage.Content = content;

            AddToken(requestMessage, authorizationToken);

            var response = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T item, string authorizationToken = null)
        {
            testOutputHelper.WriteLine($"Put call to {url}");

            var content = new StringContent(JsonSerializer.Serialize(item));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(jsonMediaType);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, new Uri(url, UriKind.RelativeOrAbsolute));
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));
            requestMessage.Content = content;

            AddToken(requestMessage, authorizationToken);

            var response = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url, string authorizationToken = null)
        {
            testOutputHelper.WriteLine($"Get call to {url}");

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, new Uri(url, UriKind.RelativeOrAbsolute));
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            AddToken(requestMessage, authorizationToken);

            var response = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

            return response;
        }

        private void AddToken(HttpRequestMessage httpRequestMessage, string authorizationToken)
        {
            if (!string.IsNullOrEmpty(authorizationToken))
            {
                testOutputHelper.WriteLine($"Add authorization token: {authorizationToken}");
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
            }
        }
    }
}