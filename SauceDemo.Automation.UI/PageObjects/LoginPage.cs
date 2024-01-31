using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SauceDemo.Automation.UI.PageObjects
{
    public class LoginPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "user-name")]
        private IWebElement UserNameInput;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement PasswordInput;

        [FindsBy(How = How.CssSelector, Using = ".btn_action")]
        private IWebElement LoginButton;

        [FindsBy(How = How.Id, Using = "react-burger-menu-btn")]
        private IWebElement BurgerMenuButton;

        [FindsBy(How = How.Id, Using = "logout_sidebar_link")]
        private IWebElement LogoutLink;

        public void Login(string username, string password)
        {
            UserNameInput.SendKeys(username);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }

        public void Logout()
        {
            BurgerMenuButton.Click();
            LogoutLink.Click();
        }

        public bool IsLoggedIn()
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inventory_filter_container"))).Displayed;
        }

        public bool IsLoggedOut()
        {
            return Wait.Unti(ExpectedConditions.ElementIsVisible(LoginButton)).Displayed);
        }
    }
}