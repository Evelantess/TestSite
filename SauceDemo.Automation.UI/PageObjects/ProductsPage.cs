using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Automation.UI.PageObjects
{
    public class ProductsPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".inventory_item button")]
        private IList<IWebElement> ProductAddToCartButtons;

        [FindsBy(How = How.CssSelector, Using = ".shopping_cart_badge)"]
        private IWebElement CartBadge;

        public void AddProductToCart(string productName)
        {
            var product = ProductAddToCartButtons
                .FirstOrDefault(button => button.FindElement(By.XPath($"ancestor::div[@class='inventory_item']//div[@class='inventory_item_name'][contains(text(), '{productName}')]")) != null);

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
