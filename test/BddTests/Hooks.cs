using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace GloomhavenTracker.BddTests
{
    [Binding]
    public class Hooks
    {
        public IWebDriver WebDriver { get; private set; }
        private static Process process;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"cd ../../../../../src && dotnet run\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            process.Kill();
        }

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