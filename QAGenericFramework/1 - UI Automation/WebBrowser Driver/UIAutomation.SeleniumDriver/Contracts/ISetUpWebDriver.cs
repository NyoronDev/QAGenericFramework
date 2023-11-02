using CrossLayer.Models.UIScenario;
using OpenQA.Selenium;

namespace UIAutomation.WebDriver.Contracts
{
    public interface ISetUpWebDriver
    {
        IWebDriver WebDriver { get; }

        void CloseWebDriver();

        void NavigateToUrl(string url);

        void CreateWebDriver(ScenarioProperties scenarioProperties);
    }
}
