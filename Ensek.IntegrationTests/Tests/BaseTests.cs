using Ensek.IntegrationTests.Services;
using Ensek.IntegrationTests.Utility;

namespace Ensek.IntegrationTests.Tests
{
    public class BaseTests
    {
        private EnergyPurchaseService energyPurchaseService;

        [OneTimeSetUp]
        public async Task BaseOneTimeSetUp()
        {
            WebApiClient = await ApiClientFactory.CreateWebApiClientAsync();
        }

        protected IApiClient WebApiClient { get; private set; }

        [OneTimeTearDown]
        public void Teardown()
        {
            WebApiClient?.Dispose();
        }

        protected EnergyPurchaseService EnergyPurchaseService
        {
            get { return energyPurchaseService = (energyPurchaseService == null) ? new EnergyPurchaseService(WebApiClient) : energyPurchaseService; }
        }
    }
}
