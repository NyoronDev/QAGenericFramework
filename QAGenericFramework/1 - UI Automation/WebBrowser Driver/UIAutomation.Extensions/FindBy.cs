using OpenQA.Selenium;

namespace UIAutomation.Extensions
{
    public class FindBy
    {
        public static By DataTestId(string dataTestIdToFind)
        {
            return By.CssSelector($"*[data-test-id='{dataTestIdToFind}']");
        }
    }
}