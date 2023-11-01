using BoDi;
using CrossLayer.Resources;
using CrossLayer.Resources.Contracts;
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
    public static class AppContainerExtensions
    {
        public static void RegisterMapper(this IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<ResourcesMapper, IResourcesMapper>();
        }

        public static void RegisterAPIs(this IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<StandardHttpClient, IStandardHttpClient>();
            objectContainer.RegisterTypeAs<HttpClientFactory, IHttpClientFactory>();
            objectContainer.RegisterTypeAs<ExampleRestApiClient, IExampleRestApiClient>();
        }

        public static void RegisterDatabaseRepositories(this IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<ExampleRepositoryTable, IExampleRepositoryTable>();
        }

        public static void RegisterPerformance(this IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<ReportDataGeneration, IReportDataGeneration>();
            objectContainer.RegisterTypeAs<PerformanceExampleRestApi, IPerformanceExampleRestApi>();
        }

        public static void RegisterWebBrowserPages(this IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<SetUpWebDriver, ISetUp>();
            objectContainer.RegisterTypeAs<WebPageBase, IPageBase>();
            objectContainer.RegisterTypeAs<ExamplePage, IExamplePage>();
        }
    }
}