namespace DataFactory.RestAPI.Client.Contracts.CustomHttpClient
{
    public interface IHttpClientFactory
    {
        IStandardHttpClient StandardHttpClient { get; }
    }
}