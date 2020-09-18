using BoDi;
using DataFactory.Database.Repository;
using DataFactory.Database.Repository.Contracts;
using DataFactory.Performance;
using DataFactory.Performance.ReportGeneration;
using DataFactory.RestAPI.Client;
using DataFactory.RestAPI.Client.Contracts;
using DataFactory.RestAPI.Client.Contracts.CustomHttpClient;
using DataFactory.RestAPI.Client.CustomHttpClient;
using UIAutomation.Contracts;
using UIAutomation.Contracts.Pages;
using UIAutomation.Contracts.Pages.Example;
using UIAutomation.SeleniumDriver;
using UIAutomation.SeleniumDriver.Pages;
using UIAutomation.SeleniumDriver.Pages.Example;

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
            objectContainer.RegisterTypeAs<ExampleRepositoryTable, IExampleRepositoryTable>();
        }

        public void RegisterPerformance(IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<ReportDataGeneration, IReportDataGeneration>();
            objectContainer.RegisterTypeAs<PerformanceExampleRestApi, IPerformanceExampleRestApi>();
        }

        public void RegisterWebBrowserPages(IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<SetUpWebDriver, ISetUp>();
            objectContainer.RegisterTypeAs<WebPageBase, IPageBase>();
            objectContainer.RegisterTypeAs<ExamplePage, IExamplePage>();
        }
    }
}