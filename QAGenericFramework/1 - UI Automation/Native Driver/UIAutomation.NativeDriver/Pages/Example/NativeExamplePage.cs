using OpenQA.Selenium.Appium;
using UIAutomation.NativeDriver.Contracts;
using UIAutomation.NativeDriver.Contracts.Pages.Example;
using Xunit.Abstractions;

namespace UIAutomation.NativeDriver.Pages.Example
{
    public class NativeExamplePage : NativePageBase, INativeExamplePage
    {
        #region .: Native Elements :.

        private AppiumElement exampleButton => GetElementBase("example-button-id");

        private IEnumerable<AppiumElement> exampleListText => GetElementsBase("example-card-id");

        #endregion

        public NativeExamplePage(ISetUpNativeDriver setUp, ITestOutputHelper testOutputHelper)
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
