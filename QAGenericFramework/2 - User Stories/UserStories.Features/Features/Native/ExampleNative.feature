Feature: ExampleNative
    In order to test example native functionality

@ID:984247F5-410F-42DF-86E2-D11F9C66FB5A
@Owner:NyoronDev
@Type:NativeUI
@TestCase:NNN-222
Scenario Outline: The user clicks example button and obtain result from example card
    Given The native scenario is executed with the following properties
        | PlatformExecution   | Device   | PlatformVersion   |
        | <platformExecution> | <device> | <platformVersion> |
    When The user clicks the native example button
    Then The user can check the native text 'This is a card' from example card 'exampleCard'

    Examples: 
        | platformExecution | device     | platformVersion |
        | Android           | Google.*   | 14              |
        | iOS               | iPhone.*   | 17              |