namespace Ensek.IntegrationTests.Utility
{
    public interface IApiClient : IDisposable
    {
        new void Dispose();
        Task<T> GetRequest<T>(string endpoint);
        Task<TResult> PutRequest<T, TResult>(string endpoint, T body);

        Task<TResult> PostRequest<T, TResult>(string endpoint, T body);
    }
}
