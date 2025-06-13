using Ensek.IntegrationTests.Utility;

namespace Ensek.IntegrationTests.Services
{
    public abstract class BaseService
    {
        protected BaseService(IApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        public IApiClient ApiClient { get; private set; }
    }
}
