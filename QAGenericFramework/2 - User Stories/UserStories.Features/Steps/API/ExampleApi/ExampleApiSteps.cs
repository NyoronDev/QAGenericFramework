using CrossLayer.Resources.Contracts;
using DataFactory.RestAPI.Client.Contracts;
using DataFactory.RestAPI.Entities.Common;
using DataFactory.RestAPI.Entities.ExampleRequest;
using DataFactory.RestAPI.Entities.ExampleResponse;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UserStories.Features.Steps.API.ExampleApi
{
    [Binding]
    public class ExampleApiSteps : StepsBase
    {
        private readonly IExampleRestApiClient exampleRestApiClient;
        private readonly IResourcesMapper resourcesMapper;

        private ApiGenericResponse<ExampleResponse> apiResponse;
        private ApiException apiException;

        public ExampleApiSteps(IExampleRestApiClient exampleRestApiClient, IResourcesMapper resourcesMapper)
        {
            this.exampleRestApiClient = exampleRestApiClient ?? throw new ArgumentNullException(nameof(exampleRestApiClient));
            this.resourcesMapper = resourcesMapper ?? throw new ArgumentNullException(nameof(resourcesMapper));
        }

        [Given(@"The user performs a post to example service with the following '(.*)' request")]
        public async Task TheUserPerformsAPostToExampleServiceWithTheFollowingRequest(string requestName)
        {
            var request = resourcesMapper.ObtainExampleRequest(requestName);

            await PerformPostFromExampleAsync(request);
        }

        [Given(@"The user performs an invalid post to example service with the following properties")]
        public async Task TheUserPerformsAnInvalidPostToExampleServiceWithTheFollowingProperties(Table table)
        {
            var request = table.CreateInstance<ExampleRequest>();

            await PerformPostFromExampleAsync(request);
        }

        [Then(@"The user receives a response from example service with the following '(.*)' response")]
        public void TheUserReceivesAResponseFromExampleServiceWithTheFollowingResponse(string responseName)
        {
            if (apiException != null)
            {
                throw apiException;
            }

            var expectedResponse = resourcesMapper.ObtainExampleResponse(responseName);
            var realResponse = apiResponse.Result;

            realResponse.ResponsePropertyOne.Should().Be(expectedResponse.ResponsePropertyOne);
            realResponse.ResponsePropertyTwo.Should().Be(expectedResponse.ResponsePropertyTwo);
        }

        [Then(@"The user receives an invalid response from example service with status code '(.*)' and response '(.*)'")]
        public void TheUserReceivesAnInvalidResponseFromExampleServiceWithStatusCodeAndResponse(string statusCode, string response)
        {
            if (apiException is null)
            {
                throw new ArgumentException("Invalid response expected from the service");
            }

            apiException.StatusCode.Should().Be(statusCode);
            apiException.Message.Should().Be(response);
        }

        private async Task PerformPostFromExampleAsync(ExampleRequest request)
        {
            try
            {
                apiResponse = await exampleRestApiClient.PostResultFromExampleAsync(request);
            }
            catch (ApiException ex)
            {
                apiException = ex;
            }
        }
    }
}