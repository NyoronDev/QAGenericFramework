﻿using CrossLayer.Models.UIScenario;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UIAutomation.NativeDriver.Contracts;

namespace UserStories.Features.Steps.Native
{
    [Binding]
    public class SetUpNativeSteps : StepsBase
    {
        private readonly ISetUpNativeDriver setUpNativeDriver;

        public SetUpNativeSteps(ISetUpNativeDriver setUpNativeDriver)
        {
            this.setUpNativeDriver = setUpNativeDriver;
        }

        [Given(@"The native scenario is executed with the following properties")]
        public void TheNativeScenarioIsExecutedWithTheFollowingProperties(Table table)
        {
            var properties = table.CreateInstance<ScenarioProperties>();

            setUpNativeDriver.CreateNativeDriver(properties);
        }
    }
}
