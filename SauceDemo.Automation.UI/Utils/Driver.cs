using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemo.Automation.UI.Utils
{
    public class Driver
    {
        private static IWebDriver _driver;
        public static IWebDriver GetDriver
        {
            get => _driver ??= new ChromeDriver();
            set => _driver = value;
        }

        public static void CloseDriver()
        {
            GetDriver.Close();
            GetDriver = null;
        }
    }
}