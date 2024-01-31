using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceDemo.Automation.UI.Utils
{
	public class CustomWait
	{
        private static readonly WebDriverWait Wait;
        public static WebDriverWait WaitInstance => Wait;

        static CustomWait()
        {
            // Assuming 'driver' is your WebDriver instance
            Wait = new WebDriverWait(Driver.GetDriver, TimeSpan.FromSeconds(10));
        }

        public static IWebElement WaitForElementVisible(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static bool WaitForElementToBeDisplayed(By locator)
        {
            try
            {
                return WaitForElementVisible(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool WaitForElementToBeDisplayed(IWebElement element)
        {
            try
            {
                return Wait.Until(driver => element.Displayed);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IReadOnlyCollection<IWebElement> WaitForAllElementsToBePresent(By locator)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        public static IReadOnlyList<IWebElement> WaitForAllElementsOrEmpty(By locator)
        {
            try
            {
                var elements = Wait.Until(driver => driver.FindElements(locator));

                if (elements.Count > 0)
                {
                    return elements;
                }
                else
                {
                    return new List<IWebElement>();
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Timed out waiting for elements to be present: {locator}");
                return new List<IWebElement>(); // Return an empty list on timeout
            }
        }
    }
}

