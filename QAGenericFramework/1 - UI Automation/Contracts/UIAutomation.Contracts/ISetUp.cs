using OpenQA.Selenium;

namespace UIAutomation.Contracts
{
    public interface ISetUp
    {
        IWebDriver WebDriver { get; }

        void CloseWebDriver();

        void NavigateToUrl(string url);
    }
}