# QA Generic Framework
Generic framework architecture for UI, Backend (database, api/rest, performance) and End to end testing in order to start a new automation testing project. .Net Core based

## Requirements
- Visual Studio
- Visual Studio Extension -- Specflow
- .Net Core 3.1

## Configuration
- You can use appsettings.json environment files (or add what you need it) to configure different variables on the project.
- You can also include docker and set up those files during pipeline

## Architecture
- Similar than n-layer arquitecture, the idea is divide the logic on different layers so you can decouple or deprecate layers that you won´t use in your real project.
