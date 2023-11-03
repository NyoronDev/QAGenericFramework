using CrossLayer.Models.UIScenario;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UIAutomation.NativeDriver.Contracts;

namespace UserStories.Features.Steps.Native
{
    [Binding]
    public class SetUpNativeSteps : StepsBase
    {
        private readonly ISetUpNativeDriver setUpNativeDriver;
        private readonly ScenarioContext scenarioContext;

        public SetUpNativeSteps(ISetUpNativeDriver setUpNativeDriver, ScenarioContext scenarioContext)
        {
            this.setUpNativeDriver = setUpNativeDriver;
            this.scenarioContext = scenarioContext;
        }

        [Given(@"The native scenario is executed with the following properties")]
        public void TheNativeScenarioIsExecutedWithTheFollowingProperties(Table table)
        {
            var properties = table.CreateInstance<ScenarioProperties>();
            properties.TestName = scenarioContext.ScenarioInfo.Title;
            properties.Build = $"Build-{DateOnly.FromDateTime(DateTime.UtcNow)}";

            setUpNativeDriver.CreateNativeDriver(properties);
        }
    }
}
