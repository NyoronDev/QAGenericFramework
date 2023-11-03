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
using UIAutomation.NativeDriver;
using UIAutomation.NativeDriver.Contracts;
using UIAutomation.WebDriver;
using UIAutomation.WebDriver.Contracts;
using UIAutomation.WebDriver.Contracts.Pages;
using UIAutomation.WebDriver.Contracts.Pages.Example;
using UIAutomation.WebDriver.Pages;
using UIAutomation.WebDriver.Pages.Example;

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
            objectContainer.RegisterTypeAs<SetUpWebDriver, ISetUpWebDriver>();
            objectContainer.RegisterTypeAs<WebPageBase, IPageBase>();
            objectContainer.RegisterTypeAs<ExamplePage, IExamplePage>();
        }

        public static void RegisterNativePages(this IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<SetUpNativeDriver, ISetUpNativeDriver>();
        }
    }
}