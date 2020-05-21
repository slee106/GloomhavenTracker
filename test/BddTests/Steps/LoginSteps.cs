using System;
using GloomhavenTracker.BddTests;
using GloomhavenTracker.BddTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace GloomhavenTracker.BddTests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver webDriver;
        private readonly LoginPageObjects loginPageObjects;

        public LoginSteps(Hooks webHooks, LoginPageObjects loginPageObjects)
        {
            this.webDriver = webHooks.WebDriver;
            this.loginPageObjects = loginPageObjects ?? throw new ArgumentNullException(nameof(loginPageObjects));
        }

        [Given(@"I am on the login screen")]
        public void GivenIAmOnTheLoginScreen()
        {
            webDriver.Navigate().GoToUrl("localhost:5000/account/login");
        }

        [When(@"I enter my valid credentials")]
        public void WhenIEnterMyValidCredentials()
        {
            loginPageObjects.UsernameTextBook.SendKeys("");
            loginPageObjects.PasswordTextBook.SendKeys("");
            loginPageObjects.LoginButton.Click();
        }

        [Then(@"I should be redirected to the home screen")]
        public void ThenMyTotalScoreShouldBe()
        {
            var x = webDriver.Url;
            Assert.AreEqual("https://localhost:5001/Home/Index", x);
        }

        [Given(@"I am not logged in")]
        public void GivenIAmNotLoggedIn()
        {

        }

        [When(@"I try to access the (.*)")]
        public void WhenITryToAccessTheAction(string action)
        {
            webDriver.Navigate().GoToUrl("localhost:5000/" + action);
        }

        [Then(@"I should be redirected to the login page")]
        public void ThenIShouldBeRedirectedToTheLoginPage()
        {
            Assert.IsTrue(webDriver.Url.Contains("https://localhost:5001/Account/Login"));
        }

        [Given(@"I am redirect to the login page from (.*)")]
        public void GivenIAmRedirectedToTheLoginPageFromAction(string action)
        {
            webDriver.Navigate().GoToUrl("localhost:5000/" + action);
        }

        [Then(@"I should be redirected to action: (.*)")]
        public void ThenIShoudlBeRedirectedToAction(string action)
        {
            Assert.IsTrue(webDriver.Url.Contains("https://localhost:5001/" + action));
        }

        [When(@"I enter (.*) for username")]
        public void WhenIEnterForUsername(string username)
        {
            loginPageObjects.UsernameTextBook.SendKeys(username);
        }

        [When(@"I enter (.*) for password")]
        public void WhenIEnterForPassword(string password)
        {
            loginPageObjects.PasswordTextBook.SendKeys(password);
            loginPageObjects.LoginButton.Click();
        }


        [Then(@"I should receive an error message")]
        public void ThenIShouldReceiveAnErrorMessage()
        {
            Assert.AreEqual("Invalid login attempt.", loginPageObjects.ValidationError.Text);
        }
    }
}