Feature: ExamplePerformance
    In order to test example api performance

@ID:0605CDBA-C516-4DBE-B66E-6BCADC893B04
@Owner:NyoronDev
@Type:API
@TestCase:DDD-111
Scenario: Performance example api create and get example
    Given An amount of '200' requests with the following configuration
        | Id | Requests |
        | 1  | 1        |
        | 2  | 3        |
        | 3  | 5        |
        | 4  | 7        |
        | 5  | 10       |