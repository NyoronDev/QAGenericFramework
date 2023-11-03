using FluentAssertions;
using System;
using TechTalk.SpecFlow;
using UIAutomation.NativeDriver.Contracts.Pages.Example;

namespace UserStories.Features.Steps.Native.ExamplePage
{
    [Binding]
    public class ExampleNativePageSteps
    {
        private readonly INativeExamplePage nativeExamplePage;

        public ExampleNativePageSteps(INativeExamplePage nativeExamplePage)
        {
            this.nativeExamplePage = nativeExamplePage ?? throw new ArgumentNullException(nameof(nativeExamplePage));
        }

        [When(@"The user clicks the native example button")]
        public void TheUserClicksTheNativeExampleButton()
        {
            nativeExamplePage.ClickExampleButton();
        }

        [Then(@"The user can check the native text '(.*)' from example card '(.*)'")]
        public void TheUserCanCheckTheNativeTextFromExampleCard(string expectedText, string cardName)
        {
            var realText = nativeExamplePage.ObtainExampleText(expectedText);

            realText.Should().Be(expectedText);
        }
    }
}
