using CrossLayer.Models.UIScenario;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace UIAutomation.NativeDriver.Contracts
{
    public interface ISetUpNativeDriver
    {
        AndroidDriver AndroidDriver { get; }

        IOSDriver IOSDriver { get; }

        PlatformExecution GetPlatformExecution();

        void CloseAndroidDriver();

        void CloseIOSDriver();

        void CreateNativeDriver(ScenarioProperties scenarioProperties);
    }
}
