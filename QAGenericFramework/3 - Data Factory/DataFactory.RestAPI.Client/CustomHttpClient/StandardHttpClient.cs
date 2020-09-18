using DataFactory.RestAPI.Client.Contracts.CustomHttpClient;
using Xunit.Abstractions;

namespace DataFactory.RestAPI.Client.CustomHttpClient
{
    public class StandardHttpClient : BaseHttpClient, IStandardHttpClient
    {
        public StandardHttpClient(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper, HttpClientType.StandardHttpClient)
        {
        }
    }
}