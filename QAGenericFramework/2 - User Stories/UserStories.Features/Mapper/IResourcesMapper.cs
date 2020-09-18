using DataFactory.RestAPI.Entities.ExampleRequest;
using DataFactory.RestAPI.Entities.ExampleResponse;

namespace UserStories.Features.Mapper
{
    public interface IResourcesMapper
    {
        ExampleRequest ObtainExampleRequest(string name);

        ExampleResponse ObtainExampleResponse(string name);
    }
}