using CrossLayer.Configuration;
using CrossLayer.Models.UIScenario;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using UIAutomation.NativeDriver.Contracts;
using Xunit.Abstractions;

namespace UIAutomation.NativeDriver
{
    public class SetUpNativeDriver : ISetUpNativeDriver
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly AppSettings appSettings;

        private AndroidDriver androidDriver;
        private IOSDriver iOSDriver;

        public SetUpNativeDriver(ITestOutputHelper testOutputHelper, AppSettings appSettings)
        {
            this.testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
            this.appSettings = appSettings;
        }

        public AndroidDriver AndroidDriver
        {
            get
            {
                if (androidDriver is null)
                {
                    throw new ApplicationException("Use the step 'The native scenario is executed with the following properties' to create the androidDriver");
                }

                return androidDriver;
            }
        }

        public IOSDriver IOSDriver
        {
            get
            {
                if (iOSDriver is null)
                {
                    throw new ApplicationException("Use the step 'The native scenario is executed with the following properties' to create the iosDriver");
                }

                return iOSDriver;
            }
        }

        public PlatformExecution GetPlatformExecution()
        {
            if (androidDriver is not null)
            {
                return PlatformExecution.Android;
            }

            if (iOSDriver is not null)
            {
                return PlatformExecution.iPhone;
            }

            throw new ApplicationException("Both Android and iPhone execution drivers are null");
        }

        public void CloseAndroidDriver()
        {
            testOutputHelper.WriteLine("Removing android driver");

            androidDriver?.Quit();
            androidDriver?.Dispose();
            androidDriver = null;
        }

        public void CloseIOSDriver()
        {
            testOutputHelper.WriteLine("Removing ios driver");

            iOSDriver?.Quit();
            iOSDriver?.Dispose();
            iOSDriver = null;
        }

        public void CreateNativeDriver(ScenarioProperties scenarioProperties)
        {
            switch (appSettings.ExecutionType)
            {
                case ExecutionType.OnPremise:
                    CreateDrive(scenarioProperties, false);
                    break;

                case ExecutionType.Cloud:
                    CreateDrive(scenarioProperties, true);
                    break;
            }
        }

        public void SendTestResultToCloud(bool isTestPassed)
        {
            if (appSettings.ExecutionType == ExecutionType.Cloud)
            {
                if (androidDriver is not null)
                {
                    ((IJavaScriptExecutor)androidDriver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
                }
                else if (iOSDriver is not null)
                {
                    ((IJavaScriptExecutor)iOSDriver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
                }
            }
        }

        private void CreateDrive(ScenarioProperties scenarioProperties, bool isCloud = false)
        {
            switch (scenarioProperties.PlatformExecution)
            {
                case PlatformExecution.Windows:
                    throw new ApplicationException("Method used for ios and android");

                case PlatformExecution.Android:
                    androidDriver = SetUpAndroidDriver(scenarioProperties, isCloud);
                    break;

                case PlatformExecution.iPhone:
                    iOSDriver = SetUpIOSDriver(scenarioProperties, isCloud);
                    break;
            }
        }

        private AndroidDriver SetUpAndroidDriver(ScenarioProperties scenarioProperties, bool isCloud = false)
        {
            testOutputHelper.WriteLine($"Creating a new {scenarioProperties.PlatformExecution} cloud chrome web driver");

            try
            {
                var options = CreateAppiumOptions(scenarioProperties, "application", "UiAutomator2", isCloud);

                var uri = new Uri("http://127.0.0.1:4723/wd/hub");

                if (isCloud)
                {
                    var sauceOptions = CreateSauceOptions(scenarioProperties);
                    options.AddAdditionalOption("sauce:options", sauceOptions);
                    uri = new Uri("https://ondemand.us-west-1.saucelabs.com:443/wd/hub");
                }

                var androidDriver = new AndroidDriver(uri, options);

                return androidDriver;
            }
            catch (Exception)
            {
                if (androidDriver != null)
                {
                    CloseAndroidDriver();
                }

                throw;
            }
        }

        private IOSDriver SetUpIOSDriver(ScenarioProperties scenarioProperties, bool isCloud = false)
        {
            testOutputHelper.WriteLine($"Creating a new {scenarioProperties.PlatformExecution} cloud chrome web driver");

            try
            {
                var options = CreateAppiumOptions(scenarioProperties, "application", "XCUITest", isCloud);

                var uri = new Uri("http://127.0.0.1:4723/wd/hub");

                if (isCloud)
                {
                    var sauceOptions = CreateSauceOptions(scenarioProperties);
                    options.AddAdditionalOption("sauce:options", sauceOptions);
                    uri = new Uri("https://ondemand.us-west-1.saucelabs.com:443/wd/hub");
                }

                var iOSDriver = new IOSDriver(uri, options);

                return iOSDriver;
            }
            catch (Exception)
            {
                if (iOSDriver != null)
                {
                    CloseIOSDriver();
                }

                throw;
            }
        }

        private AppiumOptions CreateAppiumOptions(ScenarioProperties scenarioProperties, string application, string automationName, bool isCloud = false)
        {
            var options = new AppiumOptions();
            options.AddAdditionalAppiumOption("platformName", scenarioProperties.PlatformExecution);
            options.AddAdditionalAppiumOption("appium:deviceName", scenarioProperties.Device);
            options.AddAdditionalAppiumOption("appium:platformVersion", scenarioProperties.PlatformVersion);
            options.AddAdditionalAppiumOption("appium:automationName", automationName);
            options.App = application;

            return options;
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

            var sauceOptions = new Dictionary<string, object> { ["username"] = user, ["accessKey"] = accessKey, ["build"] = scenarioProperties.Build, ["name"] = scenarioProperties.TestName };
            return sauceOptions;
        }
    }
}
