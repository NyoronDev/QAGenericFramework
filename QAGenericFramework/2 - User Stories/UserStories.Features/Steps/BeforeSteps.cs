using BoDi;
using CrossLayer.Configuration;
using CrossLayer.Containers;
using Dapper.FluentMap;
using DataFactory.Database.Entities.Mappers;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using TechTalk.SpecFlow;

namespace UserStories.Features.Steps
{
    [Binding]
    public class BeforeSteps
    {
        private readonly IObjectContainer objectContainer;

        public BeforeSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;

            var assemblyConfigurationAttribute = typeof(BeforeSteps).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>();
            var buildConfigurationName = assemblyConfigurationAttribute?.Configuration;

            // Inject appsettings configuration
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{buildConfigurationName}.json")
                .AddEnvironmentVariables()
                .Build();

            var appSettings = AppSettingsBuilder.GetConfiguration(configurationRoot);

            this.objectContainer.RegisterMapper();
            this.objectContainer.RegisterInstanceAs(appSettings);
        }

        [BeforeScenario]
        [Scope(Tag = "Type:API")]
        public void SetUpApiScenarios()
        {
            objectContainer.RegisterAPIs();

            // Used sometimes for API pre-steps
            SetUpDatabaseScenarios();
        }

        [BeforeScenario]
        [Scope(Tag = "Type:Database")]
        public void SetUpDatabaseScenarios()
        {
            objectContainer.RegisterDatabaseRepositories();
        }

        [BeforeScenario]
        [Scope(Tag = "Type:WebUI")]
        public void SetUpWebUI()
        {
            SetUpApiScenarios();
            objectContainer.RegisterWebBrowserPages();
        }

        [BeforeScenario]
        [Scope(Tag = "Type:Performance")]
        public void SetUpPerformanceScenarios()
        {
            objectContainer.RegisterAPIs();
            objectContainer.RegisterPerformance();

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ExampleMapper());
            });
        }
    }
}