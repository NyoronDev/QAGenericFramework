using CrossLayer.Models;
using CrossLayer.Models.Performance;
using DataFactory.Performance;
using DataFactory.Performance.ReportGeneration;
using DataFactory.RestAPI.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UserStories.Features.Steps.API.ExampleApi
{
    [Binding]
    public class ExampleApiPerformanceSteps : StepsBase
    {
        private readonly IReportDataGeneration reportDataGeneration;
        private readonly IExampleRestApiClient exampleRestApiClient;
        private readonly PerformanceActionList performanceActionList;

        public ExampleApiPerformanceSteps(IReportDataGeneration reportDataGeneration, IExampleRestApiClient exampleRestApiClient)
        {
            this.reportDataGeneration = reportDataGeneration ?? throw new ArgumentNullException(nameof(reportDataGeneration));
            this.exampleRestApiClient = exampleRestApiClient ?? throw new ArgumentNullException(nameof(exampleRestApiClient));

            performanceActionList = new PerformanceActionList();
        }

        [Given(@"An amount of '(.*)' requests with the following configuration")]
        public async Task AnAmountOfRequestsWithTheFollowingConfiguration(int totalRequests, Table totalRequestsTable)
        {
            var totalRequestsObject = totalRequestsTable.CreateSet<PerformanceTotalRequest>();

            performanceActionList.Title = $"Example performance {totalRequests}";
            performanceActionList.Filename = $"ExamplePerformance-{totalRequests}";
            performanceActionList.PerformanceActionType = PerformanceActionType.ExamplePerformance;
            performanceActionList.TotalActions = totalRequests;
            performanceActionList.StartedDate = DateTime.UtcNow;

            var totalExecutionTime = Stopwatch.StartNew();

            foreach (var request in totalRequestsObject)
            {
                var remainingRequests = totalRequests;
                var hasRemainingRequests = true;

                while (hasRemainingRequests)
                {
                    // Check remaining requests, if we have less requests than total requests, run tasks as much remaining requests we have
                    int requestsInParallel;
                    if (remainingRequests >= request.Requests)
                    {
                        requestsInParallel = request.Requests;
                    }
                    else
                    {
                        requestsInParallel = remainingRequests;
                        hasRemainingRequests = false;
                    }

                    var taskList = new List<Task>();

                    for (int i = 0; i < requestsInParallel; i++)
                    {
                        taskList.Add(ExampleRequestAction(request));
                    }

                    await Task.WhenAll(taskList);

                    remainingRequests -= request.Requests;
                }
            }

            totalExecutionTime.Stop();

            performanceActionList.ExecutionTotalTime = totalExecutionTime.ElapsedMilliseconds;
            performanceActionList.FinishedDate = DateTime.UtcNow;

            // Generate Json report
            reportDataGeneration.GeneratePerformanceJsonReport(performanceActionList);
        }

        private async Task ExampleRequestAction(PerformanceTotalRequest totalRequest)
        {
            var performanceExampleFactory = new PerformanceExampleRestApi(exampleRestApiClient, totalRequest.Requests);
            var performanceAction = await performanceExampleFactory.CreateAndObtainAnExampleAction();
            performanceAction.Id = totalRequest.Id;

            performanceActionList.PerformanceActions.Add(performanceAction);
        }
    }
}