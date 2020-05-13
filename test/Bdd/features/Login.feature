Feature: Google Search
  As a user, I want to login, so that I can access the website.
  
  Scenario: Simple Google search
    Given I click Login button
    When I enter my login details
    Then I am redirected to the home page