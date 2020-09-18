using BoDi;
using CrossLayer.Containers;
using Dapper.FluentMap;
using DataFactory.Database.Entities.Mappers;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using UserStories.Features.Mapper;

namespace UserStories.Features.Steps
{
    [Binding]
    public class BeforeSteps
    {
        private readonly IObjectContainer objectContainer;
        private readonly IAppContainer appContainer;

        public BeforeSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;

            // Inject solution container
            this.objectContainer.RegisterTypeAs<AppContainer, IAppContainer>();
            appContainer = this.objectContainer.Resolve<IAppContainer>();

            // Inject resources
            this.objectContainer.RegisterTypeAs<ResourcesMapper, IResourcesMapper>();

            // Inject appsettings configuration
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var environment = configurationRoot.GetSection("AppConfiguration")["Environment"];

            var configurationEnvironment = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            this.objectContainer.RegisterInstanceAs(configurationEnvironment);
        }

        [BeforeScenario]
        [Scope(Tag = "Type:API")]
        public void SetUpApiScenarios()
        {
            appContainer.RegisterAPIs(objectContainer);

            // Used sometimes for API pre-steps
            SetUpDatabaseScenarios();
        }

        [BeforeScenario]
        [Scope(Tag = "Type:Database")]
        public void SetUpDatabaseScenarios()
        {
            appContainer.RegisterDatabaseRepositories(objectContainer);
        }

        [BeforeScenario]
        [Scope(Tag = "Type:WebUI")]
        public void SetUpWebUI()
        {
            SetUpApiScenarios();
            appContainer.RegisterWebBrowserPages(objectContainer);
        }

        [BeforeScenario]
        [Scope(Tag = "Type:Performance")]
        public void SetUpPerformanceScenarios()
        {
            appContainer.RegisterAPIs(objectContainer);
            appContainer.RegisterPerformance(objectContainer);

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ExampleMapper());
            });
        }
    }
}