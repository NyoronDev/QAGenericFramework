using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using UIAutomation.WebDriver.Contracts;
using UIAutomation.WebDriver.Contracts.Pages.Example;
using Xunit.Abstractions;

namespace UIAutomation.WebDriver.Pages.Example
{
    public class ExamplePage : WebPageBase, IExamplePage
    {
        #region .: Web Elements :.

        private IWebElement exampleButton => GetElementBase("example-button-id");

        private IEnumerable<IWebElement> exampleListText => GetElementsBase("example-card-id");

        #endregion .: Web Elements :.

        public ExamplePage(ISetUpWebDriver setUp, ITestOutputHelper testOutputHelper)
            : base(setUp, testOutputHelper)
        {
        }

        public void ClickExampleButton()
        {
            exampleButton.Click();
        }

        public string ObtainExampleText(string elementName)
        {
            return exampleListText.First(m => m.Text == elementName).Text;
        }
    }
}