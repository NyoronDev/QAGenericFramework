using CrossLayer.Models.UIScenario;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using UIAutomation.NativeDriver.Contracts;
using UIAutomation.NativeDriver.Contracts.Pages;
using Xunit.Abstractions;

namespace UIAutomation.NativeDriver.Pages
{
    public class NativePageBase : INativePageBase
    {
        protected readonly ITestOutputHelper TestOutputHelper;

        protected readonly AndroidDriver AndroidDriver;
        protected readonly IOSDriver IOSDriver;

        private readonly PlatformExecution platformExecution;

        public NativePageBase(ISetUpNativeDriver setUpNativeDriver, ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));

            platformExecution = setUpNativeDriver.GetPlatformExecution();
            TestOutputHelper.WriteLine($"Execution with {platformExecution}");

            switch (platformExecution)
            {
                case PlatformExecution.Android:
                    AndroidDriver = setUpNativeDriver.AndroidDriver;
                    break;

                case PlatformExecution.iPhone:
                    IOSDriver = setUpNativeDriver.IOSDriver;
                    break;

            }
        }

        public AppiumElement GetElementBase(string dataTestId)
        {
            AppiumElement appiumElement = null;
            switch (platformExecution)
            {
                case PlatformExecution.Android:
                    appiumElement = AndroidDriver.FindElement(MobileBy.AccessibilityId(dataTestId));
                    break;

                case PlatformExecution.iPhone:
                    appiumElement = IOSDriver.FindElement(MobileBy.AccessibilityId(dataTestId));
                    break;
            }

            return appiumElement;
        }

        public IEnumerable<AppiumElement> GetElementsBase(string dataTestId)
        {
            IEnumerable<AppiumElement> appiumElements = null;
            switch (platformExecution)
            {
                case PlatformExecution.Android:
                    appiumElements = AndroidDriver.FindElements(MobileBy.AccessibilityId(dataTestId));
                    break;

                case PlatformExecution.iPhone:
                    appiumElements = IOSDriver.FindElements(MobileBy.AccessibilityId(dataTestId));
                    break;
            }

            return appiumElements;
        }
    }
}
