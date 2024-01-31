using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SauceDemo.Automation.UI.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SauceDemo.Automation.UI.PageObjects
{
    public class ProductsPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".inventory_item button")]
        private IList<IWebElement> ProductAddToCartButtons;

        [FindsBy(How = How.CssSelector, Using = ".shopping_cart_badge")]
        private IWebElement CartBadge;

        public void AddProductToCart(string productName)
        {
            string xpathSelector = $"//div[contains(@class, 'inventory_item') and descendant::div[contains(@class, 'inventory_item_name') and normalize-space(text()) = '{productName}']]";

            var product = ProductAddToCartButtons
                .FirstOrDefault(button => button.FindElement(By.XPath(xpathSelector))!= null);

            product?.Click();
        }

        public bool IsProductShownInCart()
        {
            return CartBadge.Text.Contains("1");
        }

        public void NavigateToCart()
        {
            CartBadge.Click();
        }
    }
}
