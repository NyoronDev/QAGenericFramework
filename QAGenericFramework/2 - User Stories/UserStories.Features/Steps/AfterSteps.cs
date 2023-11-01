using System;
using TechTalk.SpecFlow;
using UIAutomation.WebDriver.Contracts;

namespace UserStories.Features.Steps
{
    [Binding]
    public class AfterSteps
    {
        private readonly ISetUpWebDriver setUpDriver;

        public AfterSteps(ISetUpWebDriver setUpDriver)
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