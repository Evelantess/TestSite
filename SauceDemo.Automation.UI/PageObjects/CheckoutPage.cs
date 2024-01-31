using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SauceDemo.Automation.UI.Utils;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Automation.UI.PageObjects
{
 public class CheckoutPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "first-name")]
        private IWebElement FirstNameInput;

        [FindsBy(How = How.Id, Using = "last-name")]
        private IWebElement LastNameInput;

        [FindsBy(How = How.Id, Using = "postal-code")]
        private IWebElement ZipCodeInput;

        [FindsBy(How = How.CssSelector, Using = ".btn_primary.cart_button")]
        private IWebElement ContinueButton;

        [FindsBy(How = How.CssSelector, Using = ".btn_action.cart_button")]
        private IWebElement FinishButton;

        public void EnterCheckoutInformation(string firstName, string lastName, string zipCode)
        {
            FirstNameInput.SendKeys(firstName);
            LastNameInput.SendKeys(lastName);
            ZipCodeInput.SendKeys(zipCode);
            ContinueButton.Click();
        }

        public void FinishCheckout()
        {
            CustomWait.WaitInstance.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".btn_action.cart_button"))).Click();
        }

        public bool IsCheckoutComplete()
        {
            return CustomWait.WaitInstance.Until(ExpectedConditions.UrlContains("checkout-complete.html"));
        }
    }
}