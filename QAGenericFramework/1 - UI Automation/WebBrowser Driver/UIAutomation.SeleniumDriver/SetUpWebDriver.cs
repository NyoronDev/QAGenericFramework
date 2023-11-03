using CrossLayer.Configuration;
using CrossLayer.Models.UIScenario;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UIAutomation.WebDriver.Contracts;
using Xunit.Abstractions;

namespace UIAutomation.WebDriver
{
    public class SetUpWebDriver : ISetUpWebDriver
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly AppSettings appSettings;

        private IWebDriver webDriver;

        public SetUpWebDriver(ITestOutputHelper testOutputHelper, AppSettings appSettings)
        {
            this.testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
            this.appSettings = appSettings;
        }

        public IWebDriver WebDriver
        {
            get
            {
                if (webDriver is null)
                {
                    throw new ApplicationException("Use the step 'The web scenario is executed with the following properties' to create the webdriver");
                }

                return webDriver;
            }
        }

        public void CloseWebDriver()
        {
            testOutputHelper.WriteLine($"Removing web driver");

            webDriver?.Quit();
            webDriver?.Dispose();
            webDriver = null;
        }

        public void NavigateToUrl(string url)
        {
            testOutputHelper.WriteLine($"Navigate into {url}");

            WebDriver?.Navigate().GoToUrl(url);
        }

        public void CreateWebDriver(ScenarioProperties scenarioProperties)
        {
            switch (appSettings.ExecutionType)
            {
                case ExecutionType.OnPremise:
                    CreateOnPremiseWebDriver(scenarioProperties);
                    break;
                case ExecutionType.Cloud:
                    CreateCloudWebDriver(scenarioProperties);
                    break;
            }
        }

        private void CreateOnPremiseWebDriver(ScenarioProperties scenarioProperties)
        {
            switch (scenarioProperties.PlatformExecution)
            {
                case PlatformExecution.Windows:
                    webDriver = SetUpDesktopOnPremiseChromeWebDriver();
                    break;
                case PlatformExecution.Android:
                case PlatformExecution.iPhone:
                    webDriver = SetUpAndroidiOSWebDriver(scenarioProperties, isCloud: false);
                    break;
            }
        }

        private void CreateCloudWebDriver(ScenarioProperties scenarioProperties)
        {
            switch (scenarioProperties.PlatformExecution)
            {
                case PlatformExecution.Windows:
                    webDriver = SetUpDesktopCloudChromeWebDriver(scenarioProperties);
                    break;
                case PlatformExecution.Android:
                case PlatformExecution.iPhone:
                    webDriver = SetUpAndroidiOSWebDriver(scenarioProperties, isCloud: true);
                    break;
            }
        }

        private IWebDriver SetUpDesktopOnPremiseChromeWebDriver()
        {
            testOutputHelper.WriteLine("Creating a new desktop chrome web driver");

            try
            {
                var chromeFullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--window-size=1920,1080");
                chromeOptions.AddArgument("disable-inforbars");
                chromeOptions.AddArgument("use-fake-device-for-media-stream");
                chromeOptions.AddArgument("use-fake-ui-for-media-stream");

                webDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(chromeFullPath), chromeOptions, TimeSpan.FromSeconds(60));
                webDriver.Manage().Window.Maximize();
                webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                webDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(15);

                return webDriver;
            }
            catch (Exception)
            {
                if (webDriver != null)
                {
                    CloseWebDriver();
                }

                throw;
            }
        }

        private IWebDriver SetUpDesktopCloudChromeWebDriver(ScenarioProperties scenarioProperties)
        {
            testOutputHelper.WriteLine("Creating a new desktop cloud chrome web driver");

            try
            {
                var sauceOptions = CreateSauceOptions(scenarioProperties);
                var chromeOptions = new ChromeOptions { BrowserVersion = "latest", PlatformName = scenarioProperties.Device };

                chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);

                var webDriver = new RemoteWebDriver(new Uri("https://ondemand.us-west-1.saucelabs.com:443/wd/hub"), chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(60));
                webDriver.Manage().Window.Maximize();
                webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                webDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(15);

                return webDriver;
            }
            catch (Exception)
            {
                if (webDriver != null)
                {
                    CloseWebDriver();
                }

                throw;
            }
        }

        private IWebDriver SetUpAndroidiOSWebDriver(ScenarioProperties scenarioProperties, bool isCloud = false)
        {
            testOutputHelper.WriteLine($"Creating a new {scenarioProperties.PlatformExecution} cloud chrome web driver");

            try
            {
                var browser = string.Empty;
                var automationName = string.Empty;
                switch (scenarioProperties.PlatformExecution)
                {
                    case PlatformExecution.Windows:
                        throw new ApplicationException("Method used for ios and android");
                        
                    case PlatformExecution.Android:
                        browser = "Chrome";
                        automationName = "UiAutomator2";
                        break;

                    case PlatformExecution.iPhone:
                        browser = "Safari";
                        automationName = "XCUITest";
                        break;
                }
                
                var options = new AppiumOptions();
                options.AddAdditionalAppiumOption("platformName", scenarioProperties.PlatformExecution);
                options.AddAdditionalAppiumOption("browserName", browser);
                options.AddAdditionalAppiumOption("appium:deviceName", scenarioProperties.Device);
                options.AddAdditionalAppiumOption("appium:platformVersion", scenarioProperties.PlatformVersion);
                options.AddAdditionalAppiumOption("appium:automationName", automationName);

                var uri = new Uri("http://127.0.0.1:4723/wd/hub");
                if (isCloud)
                {
                    var sauceOptions = CreateSauceOptions(scenarioProperties);
                    options.AddAdditionalOption("sauce:options", sauceOptions);
                    uri = new Uri("https://ondemand.us-west-1.saucelabs.com:443/wd/hub");
                }

                IWebDriver webDriver = null;
                switch (scenarioProperties.PlatformExecution)
                {
                    case PlatformExecution.Windows:
                        throw new ApplicationException("Method used for ios and android");

                    case PlatformExecution.Android:
                        webDriver = new AndroidDriver(uri, options);
                        break;

                    case PlatformExecution.iPhone:
                        webDriver = new IOSDriver(uri, options);
                        break;
                }

                return webDriver;
            }
            catch (Exception)
            {
                if (webDriver != null)
                {
                    CloseWebDriver();
                }

                throw;
            }
        }

        private IDictionary<string, object> CreateSauceOptions(ScenarioProperties scenarioProperties)
        {
            var user = appSettings.SauceLabs.User;
            var accessKey = appSettings.SauceLabs.AccessKey;

            if (string.IsNullOrEmpty(user))
            {
                user = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            }
            if (string.IsNullOrEmpty(accessKey))
            {
                accessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            }

            var sauceOptions = new Dictionary<string, object> { ["username"] = user, ["accessKey"] = accessKey, ["build"]  = "buildId", ["name"] = "testName" };
            return sauceOptions;
        }
    }
}