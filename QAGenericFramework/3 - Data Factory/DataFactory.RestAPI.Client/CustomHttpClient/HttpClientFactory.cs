using DataFactory.RestAPI.Client.Contracts.CustomHttpClient;

namespace DataFactory.RestAPI.Client.CustomHttpClient
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClientFactory(IStandardHttpClient standardHttpClient)
        {
            this.StandardHttpClient = standardHttpClient;
        }

        public IStandardHttpClient StandardHttpClient { get; }
    }
}