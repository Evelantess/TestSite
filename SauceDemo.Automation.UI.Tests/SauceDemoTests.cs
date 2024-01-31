using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo.Automation.UI.Utils;

namespace SauceDemo.Automation.UI.Tests
{
    [TestFixture]
    public class SauceDemoTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = Driver.GetDriver;
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            // Navigate to the SauceDemo website
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Perform login with valid credentials
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("standard_user", "secret_sauce");

            // Assert that the user is logged in successfully
            Assert.IsTrue(loginPage.IsLoggedIn(), "Login failed");
        }

        [Test]
        public void LogoutAfterLogin()
        {
            // Perform login (using a helper method or fixture setup)
            LoginWithValidCredentials();

            // Logout and verify
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Logout();

            // Assert that the user is logged out successfully
            Assert.IsTrue(loginPage.IsLoggedOut(), "Logout failed");
        }

        [Test]
        public void AddProductToCart()
        {
            // Perform login (using a helper method or fixture setup)
            LoginWithValidCredentials();

            // Add a product to the cart
            ProductsPage productsPage = new ProductsPage();
            productsPage.AddProductToCart("Sauce Labs Backpack");

            // Verify that the product is added to the cart
            Assert.IsTrue(productsPage.IsProductShownInCart(), "Product not added to the cart");

            productsPage.NavigateToCart();
            CartPage cartPage = new CartPage();

            // Verify that the exact product is added to the cart
            Assert.IsTrue(cartPage.IsProductInCart("Sauce Labs Backpack"), "Product was not added to the cart");

        }

        [Test]
        public void RemoveProductFromCart()
        {
            // Perform login and add a product to the cart (using helper methods or fixture setup)
            LoginWithValidCredentials();
            AddProductToCart();

            // Remove a product from the cart
            CartPage cartPage = new CartPage(driver);
            cartPage.RemoveProductFromCart("Sauce Labs Backpack");

            // Verify that the product is removed from the cart
            Assert.IsFalse(cartPage.IsProductInCart("Sauce Labs Backpack"), "Product not removed from the cart");
        }

        [Test]
        public void NavigateToAboutPage()
        {
            // Perform login (using a helper method or fixture setup)
            LoginWithValidCredentials();

            // Navigate to the About page
            HomePage homePage = new HomePage(driver);
            homePage.NavigateToAboutPage();

            // Verify that the user is on the About page
            Assert.IsTrue(homePage.IsAboutPageOpened(), "Navigation to the About page failed");
        }

        [Test]
        public void CheckoutForProblemUser()
        {
            // Navigate to the SauceDemo website
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Log in with problem_user credentials
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("problem_user", "secret_sauce");

            // Add a product to the cart
            ProductsPage productsPage = new ProductsPage(driver);
            productsPage.AddProductToCart("Sauce Labs Backpack");

            // Go to the checkout page
            CartPage cartPage = new CartPage(driver);
            cartPage.GoToCheckout();

            // Verify the checkout process
            CheckoutPage checkoutPage = new CheckoutPage();
            checkoutPage.EnterCheckoutInformation("Tetiana", "Moskalenko", "12345");
            checkoutPage.FinishCheckout();

            // Verify the successful completion of the checkout process
            Assert.IsTrue(checkoutPage.IsCheckoutComplete(), "Checkout process failed");
        }

        [TearDown]
        public void TearDown()
        {
            // Close the browser after each test
            driver.Quit();
        }
    }
}