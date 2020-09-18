Feature: ExampleRepository
    In order to test example repository functionality

@ID:96623207-C8F7-4532-ABDE-B991902CD9E3
@Owner:NyoronDev
@Type:API
@TestCase:BBB-111
Scenario: Check different functionality for example repository
    Given The user removes all data from example repository
    When The user performs a new insert into example repository with the following properties
        | ExampleOne | ExampleTwo |
        | 1          | 2          |
    Then The user is able to obtain the created example from example repository