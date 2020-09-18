Feature: ExampleWeb
    In order to test example web functionality

@ID:481872BC-64BD-42BE-8146-D19BC0CE4329
@Owner:NyoronDev
@Type:WebUI
@TestCase:CCC-111
Scenario: The user clicks example button and obtain result from example card
    Given The user goes to example page
    When The user clicks the example button
    Then The user can check the text 'This is a card' from example card 'exampleCard'