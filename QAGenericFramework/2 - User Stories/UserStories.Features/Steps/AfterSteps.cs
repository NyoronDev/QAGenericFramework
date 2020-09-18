using System;
using TechTalk.SpecFlow;
using UIAutomation.Contracts;

namespace UserStories.Features.Steps
{
    [Binding]
    public class AfterSteps
    {
        private readonly ISetUp setUpDriver;

        public AfterSteps(ISetUp setUpDriver)
        {
            this.setUpDriver = setUpDriver ?? throw new ArgumentNullException(nameof(setUpDriver));
        }

        [AfterScenario]
        [Scope(Tag = "Type:WebUI")]
        public void AfterUIScenario()
        {
            setUpDriver.CloseWebDriver();
        }
    }
}