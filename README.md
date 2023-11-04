# QA Generic Framework
Generic framework architecture to run multiple test scenarios, is n-layer based so you can remove the layers that dont use (for example, Presentation if you dont need performance or Data Factory if you dont need Rest API / Database testing)

Using Specflow and BoDi dependency injection, you will be able to run the same scenario on multiple browser / devices. Scenarios are divided on Native (Android/iOS) using Appium and Web (Desktop/Android/iOS) using Selenium. For Native you only need to write one page object to cover both Android and iOS (both are AppiumElement based) and for Web with one page object you can cover Desktop, Android and iOS (all of them are using IWebDriver). If you have something specific for Android / iOS during Web tests, you can create extension methods to cast IWebDriver to AndroidDriver or iOSDriver.

Is already prepared to run on multiple environments (different json configuration files) and you can switch between On Premise and Cloud if you have SauceLabs account

The architecture already contains different templates to run the following tests:
    - Rest API
    - Database
    - Performance
    - UI Web with Desktop, Android and iOS
    - UI Native with Android and iOS
    - Cloud testing UI, Backend
    - End to end

## Requirements
    - Visual Studio
    - Visual Studio Extension -- Specflow
    - .Net 7
    - Appium Server for web/native mobile
    - Cloud Testing with SauceLabs

## Relevant Nuget packages
    - Appium (for mobile devices)
    - Selenium (for web)
    - Dapper (for database)
    - FluentAssertions (for asserts)
    - Polly (for retries)
    - RestSharp (for http client)
    - Specflow (for gherkin scenarios + dependency injection with BoDi)
    - Xunit (internally Specflow is using xunit)

## Architecture
Architecture is N-Layer based. The idea of this approach is that each project is different, In some cases you only need to create integration tests for microservices, in other project you only need to do UI tests so with an architecture based on layers you can encapsulate the logic in those layers and remove / add what you need to test.

### User Stories Layer
Main layer of the framework, is where test scenarios are allowed. We have the features with all tests divided by type (Api, Database, UI Web, UI Native) and the Steps, this Steps files are the ones that calls the other layers depending if they have a UI workflow (Appium / Selenium) or Data workflow (Api, Database, ...)

Steps methods needs to be as simple as possible, delegating the workflow responsibility to the other layers. This means usually steps methods calls the other layers to do the workflow and the Asserts.

BeforeSteps class is the one that pick the configuration from the json files, create the dependency injection, ... and AfterSteps class closes the Driver and send the test results to the cloud if need it.

### UI Automation Layer
Layer responsible for the UI workflow, we have two different folders. Native Driver with AndroidDriver and IOSDriver to use native application workflows and WebBrowser Driver with IWebDriver to use web application workflows (you can check all the configurations allowed inside the SetUp classes). 

Native platforms:
    - OnPremise Android
    - OnPremise iOS
    - Cloud Android
    - Cloud iOS

Web platforms
    - OnPremise Desktop
    - OnPremise Android
    - OnPremise iOS
    - Cloud Desktop
    - Cloud Android
    - Clouse iOS

For page object, you only need to make the worfklow once to run it on different platforms (real life applications usually have different workflows for web and native so you need to do one workflow for Web platforms and another workflow for Native platforms)

### Data Factory Layer
Layer responsible for the Backend workflow, here you can add as many folders as you need (depending your backend requirements) 

Actually the framework supports
    - Api (there are two different custom http clients, one directly from .Net and the other using RestSharp)
    - Database (using Dapper)
    - Performance (still using Api but including a report class to generate a json file used later in the Presentation layer to display performance results)

If more are need it (for example web services workflow) you can include it here.
Currently for performance I recommend K6 instead of a custom performance project but I keep it if someone is interested.

### Cross Layer
Used for common projects, here we have
    - Configuration (appsettings json configuration to be injected later where is need it)
    - Containers (BoDi dependency injection)
    - Models (common models that can be used for create instances from Table objects or used for the UI)
    - Resources (resource mapper class to return object based on resources, for example json files)

### Presentation Layer
Presentation layer is used to display results (could be a console application, web, ...). In this case contains the single page UI to check the performance restuls (graphs and tables)
As mention before, K6 with Datadog to display results are a better approach but I keep it if you need it.

## Contact
If you have doubts or questions (or also feedback) please feel free to contact me with email (jsernajaen@gmail.com) or Linkedin (https://www.linkedin.com/in/juansernajaen)
