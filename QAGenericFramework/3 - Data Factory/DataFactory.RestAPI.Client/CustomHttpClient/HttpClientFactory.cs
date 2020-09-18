using DataFactory.RestAPI.Client.Contracts.CustomHttpClient;
using System;

namespace DataFactory.RestAPI.Client.CustomHttpClient
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClientFactory(IStandardHttpClient standardHttpClient)
        {
            this.StandardHttpClient = standardHttpClient ?? throw new ArgumentNullException(nameof(standardHttpClient));
        }

        public IStandardHttpClient StandardHttpClient { get; }
    }
}