using DataFactory.RestAPI.Client.Common;
using DataFactory.RestAPI.Client.Contracts;
using DataFactory.RestAPI.Client.Contracts.CustomHttpClient;
using DataFactory.RestAPI.Entities.Common;
using DataFactory.RestAPI.Entities.ExampleRequest;
using DataFactory.RestAPI.Entities.ExampleResponse;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DataFactory.RestAPI.Client
{
    public class ExampleRestApiClient : RestApiClientBase, IExampleRestApiClient
    {
        private readonly string resultAttribute = "/result";

        private string exampleService => ConfigurationRoot.GetSection("AppConfiguration")["ExampleService"];

        public ExampleRestApiClient(IConfigurationRoot configurationRoot, IHttpClientFactory httpClientFactory)
            : base(configurationRoot, httpClientFactory)
        {
        }

        /// <summary>
        /// Obtain a result from example api.
        /// GET - {exampleService}/result/{id}
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        public async Task<ApiResponse> GetResultFromExampleAsync(string id)
        {
            var url = $"{exampleService}{resultAttribute}/{id}";

            var response = await HttpClientFactory.StandardHttpClient.GetAsync(url);

            return await CreateApiResponse(response);
        }

        /// <summary>
        /// Create a result from example api.
        /// POST - {exampleService}/result
        /// </summary>
        /// <param name="exampleRequest">The example request.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        public async Task<ApiGenericResponse<ExampleResponse>> PostResultFromExampleAsync(ExampleRequest exampleRequest)
        {
            var url = $"{exampleService}{resultAttribute}";

            var response = await HttpClientFactory.StandardHttpClient.PostAsync(url, exampleRequest);

            return await CreateApiGenericResponse<ExampleResponse>(response);
        }
    }
}