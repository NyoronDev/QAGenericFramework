using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using UIAutomation.Extensions;
using UIAutomation.WebDriver.Contracts;
using UIAutomation.WebDriver.Contracts.Pages;
using Xunit.Abstractions;

namespace UIAutomation.WebDriver.Pages
{
    public class WebPageBase : IPageBase
    {
        protected readonly IWebDriver WebDriver;
        protected readonly ITestOutputHelper TestOutputHelper;

        public WebPageBase(ISetUpWebDriver setUp, ITestOutputHelper testOutputHelper)
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