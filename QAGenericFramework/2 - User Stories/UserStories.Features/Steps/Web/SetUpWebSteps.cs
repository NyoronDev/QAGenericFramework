using CrossLayer.Models.UIScenario;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UIAutomation.WebDriver.Contracts;

namespace UserStories.Features.Steps.Web
{
    [Binding]
    public class SetUpWebSteps : StepsBase
    {
        private readonly ISetUpWebDriver setUpWebDriver;
        private readonly ScenarioContext scenarioContext;

        public SetUpWebSteps(ISetUpWebDriver setUpWebDriver, ScenarioContext scenarioContext)
        {
            this.setUpWebDriver = setUpWebDriver;
            this.scenarioContext = scenarioContext;
        }

        [Given(@"The web scenario is executed with the following properties")]
        public void TheWebScenarioIsExecutedWithTheFollowingProperties(Table table)
        {
            var properties = table.CreateInstance<ScenarioProperties>();
            properties.TestName = scenarioContext.ScenarioInfo.Title;
            properties.Build = $"Build-{DateOnly.FromDateTime(DateTime.UtcNow)}";

            setUpWebDriver.CreateWebDriver(properties);
        }
    }
}
