using TechTalk.SpecFlow;
using System;
using Xunit;

namespace Bdd.Steps
{
    [Binding]
    public class GoogleSearchSteps
    {
        [Given(@"I click Login button")]
        public void GivenIClickOnTheLoginButton()
        {

        }

        [When(@"I enter my login details")]
        public void WhenIEnterLoginDetails()
        {
            Console.WriteLine("When Some conditions");
        }

        [Then(@"results for (.*) are displayed")]
        public void ThenResultsForAreDisplayed(string phase)
        {
            Console.WriteLine("Then some outcome");
            Assert.True(true, "expected true fund true");
        }
    }
}