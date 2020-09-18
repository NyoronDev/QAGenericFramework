using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using UIAutomation.Contracts;
using UIAutomation.Contracts.Pages;
using UIAutomation.Extensions;
using Xunit.Abstractions;

namespace UIAutomation.SeleniumDriver.Pages
{
    public class WebPageBase : IPageBase
    {
        protected readonly IWebDriver WebDriver;
        protected readonly ITestOutputHelper TestOutputHelper;

        public WebPageBase(ISetUp setUp, ITestOutputHelper testOutputHelper)
        {
            WebDriver = setUp.WebDriver ?? throw new ArgumentNullException(nameof(setUp));
            TestOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        protected IWebElement GetElementBase(string dataTestId, int waitSeconds = 1)
        {
            TestOutputHelper.WriteLine($"Wait for element {dataTestId}");
            return WebDriver.WaitUntilFindElement(FindBy.DataTestId(dataTestId), waitSeconds);
        }

        protected IEnumerable<IWebElement> GetElementsBase(string dataTestId, int waitSeconds = 1)
        {
            TestOutputHelper.WriteLine($"Wait for element {dataTestId}");
            return WebDriver.WaitUntilFindElements(FindBy.DataTestId(dataTestId), waitSeconds);
        }
    }
}