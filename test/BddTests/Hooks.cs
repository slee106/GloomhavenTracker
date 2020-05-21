using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace GloomhavenTracker.BddTests
{
    [Binding]
    public class Hooks
    {
        public IWebDriver WebDriver { get; private set; }

        [BeforeScenario]
        public void BeforeScenario()
        {
            WebDriver = new ChromeDriver();
            WebDriver.Manage().Window.Maximize();
            WebDriver.Navigate().GoToUrl("localhost:5000");
            var advancedButton = WebDriver.FindElement(By.Id("details-button"));
            if (advancedButton != null)
            {
                advancedButton.Click();
                WebDriver.FindElement(By.Id("proceed-link")).Click();
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}