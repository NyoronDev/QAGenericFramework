using CrossLayer.Configuration;
using CrossLayer.Models.UIScenario;
using OpenQA.Selenium;
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
                    break;
                case PlatformExecution.iPhone:
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
                    break;
                case PlatformExecution.iPhone:
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
            testOutputHelper.WriteLine("Creating a new cloud chrome web driver");

            try
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

                var sauceOptions = new Dictionary<string, object> { ["username"] = user, ["accessKey"] = accessKey };
                var chromeOptions = new ChromeOptions { BrowserVersion = "latest", PlatformName = scenarioProperties.Device };

                chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);

                var webDriver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"), chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(60));
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
    }
}