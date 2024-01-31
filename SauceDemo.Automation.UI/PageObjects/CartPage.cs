using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SauceDemo.Automation.UI.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SauceDemo.Automation.UI.PageObjects
{
 public class CartPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".cart_item button")]
        private IList<IWebElement> ProductRemoveButtons;

        [FindsBy(How = How.CssSelector, Using = ".checkout_button")]
        private IWebElement CheckoutButton;

        public void RemoveProductFromCart(string productName)
        {
            var removeButton = ProductRemoveButtons
                .FirstOrDefault(button => button.FindElement(By.XPath($"ancestor::div[@class='cart_item']//div[@class='inventory_item_name'][contains(text(), '{productName}')]")) != null);

            removeButton?.Click();
        }

        public void GoToCheckout()
        {
            CheckoutButton.Click();
        }

        public bool IsProductInCart(string productName)
        {
            var cartItems = CustomWait.WaitForAllElementsOrEmpty(By.CssSelector(".cart_item"));

            // Check if the exact product is in the cart
            return cartItems.Any(item => item.Text.Contains(productName));
        }
    }
}