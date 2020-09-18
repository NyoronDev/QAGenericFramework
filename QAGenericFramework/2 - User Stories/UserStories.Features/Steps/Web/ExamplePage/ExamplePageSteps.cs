using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using TechTalk.SpecFlow;
using UIAutomation.Contracts;
using UIAutomation.Contracts.Pages.Example;

namespace UserStories.Features.Steps.Web.ExamplePage
{
    [Binding]
    public class ExamplePageSteps : StepsBase
    {
        private readonly ISetUp setUpDriver;
        private readonly IExamplePage examplePage;
        private readonly IConfigurationRoot configurationRoot;

        private string examplePageUrl => configurationRoot.GetSection("AppConfiguration")["ExampleWebPage"];

        public ExamplePageSteps(IExamplePage examplePage, ISetUp setUpDriver, IConfigurationRoot configurationRoot)
        {
            this.setUpDriver = setUpDriver ?? throw new ArgumentNullException(nameof(setUpDriver));
            this.examplePage = examplePage ?? throw new ArgumentNullException(nameof(examplePage));
            this.configurationRoot = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));
        }

        [Given(@"The user goes to example page")]
        public void TheUserGoesToExamplePage()
        {
            setUpDriver.NavigateToUrl(examplePageUrl);
        }

        [When(@"The user clicks the example button")]
        public void TheUserClicksTheExampleButton()
        {
            examplePage.ClickExampleButton();
        }

        [Then(@"The user can check the text '(.*)' from example card '(.*)'")]
        public void TheUserCanCheckTheTextFromExampleCard(string expectedText, string cardName)
        {
            var realText = examplePage.ObtainExampleText(cardName);

            realText.Should().Be(expectedText);
        }
    }
}