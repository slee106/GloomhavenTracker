using GloomhavenTracker.BddTests;
using OpenQA.Selenium;

namespace GloomhavenTracker.BddTests.PageObjects
{
    public class LoginPageObjects
    {
        public IWebElement UsernameTextBook
        {
            get
            {
                return webDriver.FindElement(By.Id("Username"));
            }
        }

        public IWebElement PasswordTextBook
        {
            get
            {
                return webDriver.FindElement(By.Id("Password"));
            }
        }

        public IWebElement LoginButton
        {
            get
            {
                return webDriver.FindElement(By.Id("LoginButton"));
            }
        }

        public IWebElement ValidationError
        {
            get
            {
                return webDriver.FindElement(By.Id("validation-error"));
            }
        }

        private readonly IWebDriver webDriver;
        public LoginPageObjects(Hooks webHooks)
        {
            this.webDriver = webHooks.WebDriver;
        }
    }
}