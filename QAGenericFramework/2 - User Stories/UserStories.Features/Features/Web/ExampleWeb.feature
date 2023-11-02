Feature: ExampleWeb
    In order to test example web functionality

@ID:481872BC-64BD-42BE-8146-D19BC0CE4329
@Owner:NyoronDev
@Type:WebUI
@TestCase:CCC-111
Scenario Outline: The user clicks example button and obtain result from example card
    Given The web scenario is executed with the following properties
        | PlatformExecution   | Device   |
        | <platformExecution> | <device> |
    And The user goes to example page
    When The user clicks the example button
    Then The user can check the text 'This is a card' from example card 'exampleCard'

    Examples: 
        | platformExecution | device     |
        | Windows           | Windows 10 |
        | Android           | Pixel 8    |
        | iOS               | Iphone 15  |