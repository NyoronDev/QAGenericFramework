using CrossLayer.Resources.Contracts;
using DataFactory.RestAPI.Entities.ExampleRequest;
using DataFactory.RestAPI.Entities.ExampleResponse;
using System.Text.Json;
using Xunit.Abstractions;

namespace CrossLayer.Resources
{
    public class ResourcesMapper : IResourcesMapper
    {
        private const string resourcesPath = "Resources";

        private readonly ITestOutputHelper testOutputHelper;

        public ResourcesMapper(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        public ExampleRequest ObtainExampleRequest(string name)
        {
            var requestExampleList = LoadResourcesFiles<ExampleRequest>(Path.Combine(resourcesPath, "ExampleRequests.json"));
            var requestExample = requestExampleList.First(m => m.Name == name);

            return requestExample;
        }

        public ExampleResponse ObtainExampleResponse(string name)
        {
            var responseExampleList = LoadResourcesFiles<ExampleResponse>(Path.Combine(resourcesPath, "ExampleResponses.json"));
            var responseExample = responseExampleList.First(m => m.Name == name);

            return responseExample;
        }

        private IEnumerable<T> LoadResourcesFiles<T>(string filePath) where T : class
        {
            testOutputHelper.WriteLine($"Read file {filePath}");

            var fileText = File.ReadAllText(filePath);
            var result = JsonSerializer.Deserialize<List<T>>(fileText);

            return result;
        }
    }
}
