using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace UIAutomation.Extensions
{
    public static class ElementExtensions
    {
        public static IWebElement GetChildElement(this IWebElement element, By by, double waitSeconds = 1)
        {
            var attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    var wait = element.GetWaitUntil(waitSeconds);
                    var findElement = wait.Until(x => x.FindElement(by));
                    if (findElement is null)
                    {
                        throw new NotFoundException($"Element not found");
                    }

                    return findElement;
                }
                catch (Exception)
                {
                    attempts++;
                }
            }

            return null;
        }

        public static IEnumerable<IWebElement> GetChildElements(this IWebElement element, By by, double waitSeconds = 1)
        {
            var attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    var wait = element.GetWaitUntil(waitSeconds);
                    var findElement = wait.Until(x => x.FindElements(by));
                    if (findElement is null)
                    {
                        throw new NotFoundException($"Elements not found");
                    }

                    return findElement;
                }
                catch (Exception)
                {
                    attempts++;
                }
            }

            return null;
        }

        private static IWait<IWebElement> GetWaitUntil(this IWebElement element, double waitSeconds)
        {
            var wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(waitSeconds);

            return wait;
        }
    }
}