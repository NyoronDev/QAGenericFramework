using DataFactory.RestAPI.Entities.Common;
using DataFactory.RestAPI.Entities.ExampleRequest;
using DataFactory.RestAPI.Entities.ExampleResponse;
using System.Threading.Tasks;

namespace DataFactory.RestAPI.Client.Contracts
{
    public interface IExampleRestApiClient
    {
        /// <summary>
        /// Obtain a result from example api.
        /// GET - {exampleService}/result/{id}
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<ApiResponse> GetResultFromExampleAsync(string id);

        /// <summary>
        /// Create a result from example api.
        /// POST - {exampleService}/result
        /// </summary>
        /// <param name="exampleRequest">The example request.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<ApiGenericResponse<ExampleResponse>> PostResultFromExampleAsync(ExampleRequest exampleRequest);
    }
}