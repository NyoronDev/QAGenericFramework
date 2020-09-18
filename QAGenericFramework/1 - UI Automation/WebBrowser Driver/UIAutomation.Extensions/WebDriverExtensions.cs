using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UIAutomation.Extensions
{
    public static class WebDriverExtensions
    {
        public static IWebElement WaitUntilFindElement(this IWebDriver webDriver, By by, double waitSeconds = 1)
        {
            var attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitSeconds);
                    var webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(waitSeconds));
                    var findElement = webDriverWait.Until(m => m.FindElement(by));

                    if (findElement is null)
                    {
                        throw new NotFoundException($"Element not found");
                    }

                    return findElement;
                }
                catch
                {
                    attempts++;
                }
            }

            return null;
        }

        public static IEnumerable<IWebElement> WaitUntilFindElements(this IWebDriver webDriver, By by, double waitSeconds = 1)
        {
            var attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitSeconds);
                    var webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(waitSeconds));
                    var findElements = webDriverWait.Until(m => m.FindElements(by));

                    if (findElements is null || !findElements.Any())
                    {
                        throw new NotFoundException($"Elements not found");
                    }

                    return findElements;
                }
                catch
                {
                    attempts++;
                }
            }

            return null;
        }

        public static void ScrollToElement(this IWebDriver webDriver, IWebElement element, bool isScrollBottom = false)
        {
            if (isScrollBottom)
            {
                webDriver.ExecuteJavaScript("arguments[0].scrollIntoView();", element);
            }
            else
            {
                webDriver.ExecuteJavaScript("arguments[0].scrollIntoView(false);", element);
            }
        }
    }
}