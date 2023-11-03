using System;
using TechTalk.SpecFlow;
using UIAutomation.NativeDriver.Contracts;
using UIAutomation.WebDriver.Contracts;

namespace UserStories.Features.Steps
{
    [Binding]
    public class AfterSteps
    {
        private readonly ISetUpWebDriver setUpWebDriver;
        private readonly ISetUpNativeDriver setUpNativeDriver;

        public AfterSteps(ISetUpWebDriver setUpWebDriver, ISetUpNativeDriver setUpNativeDriver)
        {
            this.setUpWebDriver = setUpWebDriver ?? throw new ArgumentNullException(nameof(setUpWebDriver));
            this.setUpNativeDriver = setUpNativeDriver ?? throw new ArgumentNullException(nameof(setUpNativeDriver));
        }

        [AfterScenario]
        [Scope(Tag = "Type:WebUI")]
        [Scope(Tag = "Type:NativeUI")]
        public void AfterUIScenario()
        {
            setUpWebDriver.CloseWebDriver();
            setUpNativeDriver.CloseIOSDriver();
            setUpNativeDriver.CloseAndroidDriver();
        }
    }
}