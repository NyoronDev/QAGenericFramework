using DataFactory.RestAPI.Entities.ExampleRequest;
using DataFactory.RestAPI.Entities.ExampleResponse;

namespace CrossLayer.Resources.Contracts
{
    public interface IResourcesMapper
    {
        ExampleRequest ObtainExampleRequest(string name);

        ExampleResponse ObtainExampleResponse(string name);
    }
}
