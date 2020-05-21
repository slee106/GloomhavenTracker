Feature: Login
       In order to gain access to the website
       As a user with credentials
       I want to try logging in

@Login
Scenario: valid login no redirect
       Given I am on the login screen
       When I enter my valid credentials
       Then I should be redirected to the home screen

@Login
Scenario: redirect to login
       Given I am not logged in
       When I try to access the <action>
       Then I should be redirected to the login page

       Examples:
       | action |
       | Character/Create    |
       | Character/Detail    |
       | Character/Delete     |
       | Item/Shop     |
       | Item/Index     |
       | Item/AddItemToCharacter     |
       | Item/Create     |
       | Item/Detail     |
       | Item/EquipItem     |
       | Item/UnequipItem     |
       | Item/SellItem     |
       | Manage/Index     |
       | Manage/ChangePassword     |
       | Party/Index     |
       | Party/Create     |
       | Party/Detail     |
       | Party/Delete     |
       | Party/Edit     |

@Login
Scenario: valid login redirect
       Given I am redirect to the login page from <action>
       When I enter my valid credentials
       Then I should be redirected to action: <action>

       Examples:
       | action |
       | Character/Create    |
       | Character/Detail    |
       | Character/Delete     |
       | Item/Shop     |
       | Item/Index     |
       | Item/AddItemToCharacter     |
       | Item/Create     |
       | Item/Detail     |
       | Item/EquipItem     |
       | Item/UnequipItem     |
       | Item/SellItem     |
       | Manage/Index     |
       | Manage/ChangePassword     |
       | Party/Index     |
       | Party/Create     |
       | Party/Detail     |
       | Party/Delete     |
       | Party/Edit     |

@Login
Scenario: invalid login
       Given I am on the login screen
       When I enter <username> for username
       And I enter <password> for password
       Then I should receive an error message

       Examples:
       | username | password |
       | BadUsername |   |
       |  | BadPassword  |
       | BadUsername | BadPassword  |