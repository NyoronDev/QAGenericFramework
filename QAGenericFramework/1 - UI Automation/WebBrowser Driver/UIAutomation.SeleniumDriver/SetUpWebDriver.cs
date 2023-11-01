using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using UIAutomation.Contracts;
using Xunit.Abstractions;

namespace UIAutomation.SeleniumDriver
{
    public class SetUpWebDriver : ISetUp
    {
        private readonly ITestOutputHelper testOutputHelper;

        private IWebDriver webDriver;

        public SetUpWebDriver(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        public IWebDriver WebDriver
        {
            get
            {
                if (webDriver is null)
                {
                    webDriver = CreateWebDriver();
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

        private IWebDriver CreateWebDriver()
        {
            // Could be used to instanciate more types of web driver, use AppSettings and set up the configuration propertly
            return SetUpChromeWebDriver();
        }

        private IWebDriver SetUpChromeWebDriver()
        {
            testOutputHelper.WriteLine("Creating a new chrome web driver");

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
    }
}