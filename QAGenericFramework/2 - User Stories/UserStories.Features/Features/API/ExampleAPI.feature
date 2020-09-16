Feature: ExampleAPI
    In order to test example api functionality

@ID:A48EA9AC-D5CD-47B4-8782-F3EB010BF5F1
@Owner:NyoronDev
@Type:API
@TestCase:AAA-111
Scenario Outline: This is an API test template
    Given The user performs a post to example service with the following '<request>' request
    Then The user receives a response from example service with the following '<response>' response
    Examples: 
        | request           | response           |
        | Example request 1 | Example response 1 |
        | Example request 2 | Example response 2 |

@ID:A48EA9AC-D5CD-47B4-8782-F3EB010BF5F1
@Owner:NyoronDev
@Type:API
@TestCase:AAA-112
Scenario: This is an Invalid API test template
    Given The user performs an invalid post to example service with the following properties
        | Name            | RequestPropertyOne | RequestPropertyTwo   |
        | Invalid request | Invalid property 1 | Invalid property Two |
    Then The user receives an invalid response from example service with status code '400' and response 'Invalid request'