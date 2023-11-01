using CrossLayer.Configuration;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;
using UIAutomation.WebDriver.Contracts;
using UIAutomation.WebDriver.Contracts.Pages.Example;

namespace UserStories.Features.Steps.Web.ExamplePage
{
    [Binding]
    public class ExamplePageSteps : StepsBase
    {
        private readonly ISetUpWebDriver setUpDriver;
        private readonly IExamplePage examplePage;

        private readonly string examplePageUrl;

        public ExamplePageSteps(IExamplePage examplePage, ISetUpWebDriver setUpDriver, AppSettings appSettings)
        {
            this.setUpDriver = setUpDriver ?? throw new ArgumentNullException(nameof(setUpDriver));
            this.examplePage = examplePage ?? throw new ArgumentNullException(nameof(examplePage));

            examplePageUrl = appSettings.AppConfiguration.ExampleWebPage;
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