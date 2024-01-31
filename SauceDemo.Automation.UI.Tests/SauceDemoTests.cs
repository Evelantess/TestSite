using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemo.Automation.UI.PageObjects;
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
            LoginPage loginPage = new LoginPage();
            loginPage.Login("standard_user", "secret_sauce");

            // Assert that the user is logged in successfully
            Assert.That(loginPage.IsLoggedIn(), "Login failed");
        }

        [Test]
        public void LogoutAfterLogin()
        {
            // Perform login (using a helper method or fixture setup)
            LoginWithValidCredentials();

            // Logout and verify
            LoginPage loginPage = new LoginPage();
            loginPage.Logout();

            // Assert that the user is logged out successfully
            Assert.That(loginPage.IsLoggedOut(), "Logout failed");
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
            Assert.That(productsPage.IsProductShownInCart(), "Product not added to the cart");

            productsPage.NavigateToCart();
            CartPage cartPage = new CartPage();

            // Verify that the exact product is added to the cart
            Assert.That(cartPage.IsProductInCart("Sauce Labs Backpack"), "Product was not added to the cart");

        }

        [Test]
        public void RemoveProductFromCart()
        {
            // Perform login and add a product to the cart (using helper methods or fixture setup)
            LoginWithValidCredentials();
            AddProductToCart();

            // Remove a product from the cart
            CartPage cartPage = new CartPage();
            cartPage.RemoveProductFromCart("Sauce Labs Backpack");

            // Verify that the product is removed from the cart
            Assert.That(cartPage.IsProductInCart("Sauce Labs Backpack") == false, "Product not removed from the cart");
        }

        [Test]
        public void CheckoutForProblemUser()
        {
            // Navigate to the SauceDemo website
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Log in with problem_user credentials
            LoginPage loginPage = new LoginPage();
            loginPage.Login("problem_user", "secret_sauce");

            // Add a product to the cart
            ProductsPage productsPage = new ProductsPage();
            productsPage.AddProductToCart("Sauce Labs Backpack");

            // Go to the checkout page
            CartPage cartPage = new CartPage();
            cartPage.GoToCheckout();

            // Verify the checkout process
            CheckoutPage checkoutPage = new CheckoutPage();
            checkoutPage.EnterCheckoutInformation("Tetiana", "Moskalenko", "12345");
            checkoutPage.FinishCheckout();

            // Verify the successful completion of the checkout process
            Assert.That(checkoutPage.IsCheckoutComplete(), "Checkout process failed");
        }

        [TearDown]
        public void TearDown()
        {
            // Close the browser after each test
            driver.Quit();
        }
    }
}