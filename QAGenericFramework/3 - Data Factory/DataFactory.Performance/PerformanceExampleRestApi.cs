using CrossLayer.Models;
using DataFactory.RestAPI.Client.Contracts;
using DataFactory.RestAPI.Entities.Common;
using DataFactory.RestAPI.Entities.ExampleRequest;
using System;
using System.Threading.Tasks;

namespace DataFactory.Performance
{
    public class PerformanceExampleRestApi : IPerformanceExampleRestApi
    {
        private readonly IExampleRestApiClient exampleRestApiClient;
        private readonly int totalRequests;

        private PerformanceAction performanceAction;

        public PerformanceExampleRestApi(IExampleRestApiClient exampleRestApiClient, int totalRequests)
        {
            this.exampleRestApiClient = exampleRestApiClient ?? throw new ArgumentNullException(nameof(exampleRestApiClient));
        }

        public async Task<PerformanceAction> CreateAndObtainAnExampleAction()
        {
            performanceAction = new PerformanceAction { Description = "Create and obtain an example", TotalRequests = totalRequests };

            // Step 1: Post request
            await CreateAnExample();

            // Step 2: Get request
            await ObtainAnExample();

            performanceAction.ActionEnded = DateTime.UtcNow;

            return performanceAction;
        }

        private async Task CreateAnExample()
        {
            var subAction = new PerformanceSubAction { Type = "Post request" };

            try
            {
                await exampleRestApiClient.PostResultFromExampleAsync(new ExampleRequest());
            }
            catch (ApiException ex)
            {
                subAction.HasErrors = true;
                subAction.Error = $"{ex.StatusCode} -- {ex.Response}";
            }
            catch (Exception ex)
            {
                subAction.HasErrors = true;
                subAction.Error = ex.Message;
            }
            finally
            {
                subAction.StopWatch();
                performanceAction.PerformanceSubActions.Add(subAction);
            }
        }

        private async Task ObtainAnExample()
        {
            var subAction = new PerformanceSubAction { Type = "Get request" };

            try
            {
                await exampleRestApiClient.GetResultFromExampleAsync("IdExample");
            }
            catch (ApiException ex)
            {
                subAction.HasErrors = true;
                subAction.Error = $"{ex.StatusCode} -- {ex.Response}";
            }
            catch (Exception ex)
            {
                subAction.HasErrors = true;
                subAction.Error = ex.Message;
            }
            finally
            {
                subAction.StopWatch();
                performanceAction.PerformanceSubActions.Add(subAction);
            }
        }
    }
}