using CrossLayer.Models.UIScenario;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UIAutomation.WebDriver.Contracts;

namespace UserStories.Features.Steps.Web
{
    [Binding]
    public class SetUpWebSteps : StepsBase
    {
        private readonly ISetUpWebDriver setUpWebDriver;

        public SetUpWebSteps(ISetUpWebDriver setUpWebDriver)
        {
            this.setUpWebDriver = setUpWebDriver;
        }

        [Given(@"The web scenario is executed with the following properties")]
        public void TheWebScenarioIsExecutedWithTheFollowingProperties(Table table)
        {
            var properties = table.CreateInstance<ScenarioProperties>();

            setUpWebDriver.CreateWebDriver(properties);
        }
    }
}
