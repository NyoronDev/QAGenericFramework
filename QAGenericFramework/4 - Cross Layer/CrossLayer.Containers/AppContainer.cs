using BoDi;
using DataFactory.RestAPI.Client;
using DataFactory.RestAPI.Client.Contracts;
using DataFactory.RestAPI.Client.Contracts.CustomHttpClient;
using DataFactory.RestAPI.Client.CustomHttpClient;

namespace CrossLayer.Containers
{
    public class AppContainer : IAppContainer
    {
        public void RegisterAPIs(IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<StandardHttpClient, IStandardHttpClient>();
            objectContainer.RegisterTypeAs<HttpClientFactory, IHttpClientFactory>();
            objectContainer.RegisterTypeAs<ExampleRestApiClient, IExampleRestApiClient>();
        }

        public void RegisterDatabaseRepositories(IObjectContainer objectContainer)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterPerformance(IObjectContainer objectContainer)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterWebBrowserPages(IObjectContainer objectContainer)
        {
            throw new System.NotImplementedException();
        }
    }
}