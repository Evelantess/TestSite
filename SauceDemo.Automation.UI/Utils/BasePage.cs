using System;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Automation.UI.Utils
{
    public class BasePage
    {
        private readonly WebDriverWait _wait;

        public BasePage()
        {
            _wait = new WebDriverWait(Driver.GetDriver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(Driver.GetDriver, this);
        }

    }
}
