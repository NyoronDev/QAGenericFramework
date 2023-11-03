using System;
using System.Threading.Tasks;
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

        private readonly ScenarioContext scenarioContext;

        public AfterSteps(ISetUpWebDriver setUpWebDriver, ISetUpNativeDriver setUpNativeDriver, ScenarioContext scenarioContext)
        {
            this.setUpWebDriver = setUpWebDriver ?? throw new ArgumentNullException(nameof(setUpWebDriver));
            this.setUpNativeDriver = setUpNativeDriver ?? throw new ArgumentNullException(nameof(setUpNativeDriver));
            this.scenarioContext = scenarioContext;
        }

        [AfterScenario]
        [Scope(Tag = "Type:WebUI")]
        public void AfterWebUIScenario()
        {
            var isTestPassed = scenarioContext.TestError is null;
            setUpWebDriver.SendTestResultToCloud(isTestPassed);
            setUpWebDriver.CloseWebDriver();
        }

        [Scope(Tag = "Type:NativeUI")]
        public void AfterNativeUIScenario()
        {
            var isTestPassed = scenarioContext.TestError is null;
            setUpWebDriver.SendTestResultToCloud(isTestPassed);
            setUpNativeDriver.CloseIOSDriver();
            setUpNativeDriver.CloseAndroidDriver();
        }
    }
}