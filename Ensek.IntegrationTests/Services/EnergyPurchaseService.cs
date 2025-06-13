using Ensek.IntegrationTests.Models;
using Ensek.IntegrationTests.Utility;
using System.Net.Http;
using System.Text.Json;

namespace Ensek.IntegrationTests.Services
{
    public class EnergyPurchaseService : BaseService
    {
        public EnergyPurchaseService(IApiClient apiClient)
            : base(apiClient)
        {
        }

        public async Task<MessageResponse> ResetEnergyPurchaseData()
        {
            var url = string.Format(EndpointRoute.ResetEnergy);
            var resetResponse = await ApiClient.PostRequest<object, MessageResponse>(url, string.Empty);

            return resetResponse;
        }

        public async Task<EnergyType> GetAllEnergyData()
        {
            var url = EndpointRoute.Energy;

            var energyData = await ApiClient.GetRequest<EnergyType>(url);

            return energyData;
        }

        public async Task<MessageResponse> MakePurchase(int energyId, int quantity)
        {
            var url = EndpointRoute.BuyEnergy + $"/{energyId}/{quantity}";

            var purchaseResponse = await ApiClient.PutRequest<object, MessageResponse>(url, string.Empty);

            return purchaseResponse;
        }
    }
}
