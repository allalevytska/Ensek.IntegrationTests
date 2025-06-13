using Ensek.IntegrationTests.Models;
using Ensek.IntegrationTests.Services;
using Ensek.IntegrationTests.Utility;
using FluentAssertions;
namespace Ensek.IntegrationTests.Tests
{
    public class EnergyPurchaseTests : BaseTests
    {
        [TestCase(3, 100)] // Test for Electric
        [TestCase(1, 50)]  // Test for Gas
        [TestCase(4, 5)]   // Test for Oil
        public async Task VerifyFuelPurchaseIsSuccessfull(int energyId, int quantity)
        {
            // Step 1: Reset the Energy Data
            var resetResponse = await EnergyPurchaseService.ResetEnergyPurchaseData();
            resetResponse.Message.Should().NotBeNullOrWhiteSpace("Reset operation should succeed.");
            resetResponse.Message.Should().Contain("Success");

            // Step 2: Fetch the Initial Data Dynamically
            var energyData = await EnergyPurchaseService.GetAllEnergyData();

            // Find the details for the specific energy type based on energyId
            var energyDetails = GetEnergyDetailsById(energyData, energyId);

            energyDetails.Should().NotBeNull($"Energy data for energyId {energyId} should exist.");
            var initialQuantity = energyDetails.QuantityOfUnits;
            var unitType = energyDetails.UnitType;
            var pricePerUnit = energyDetails.PricePerUnit;

            // Step 3: Execute the Purchase
            var response = await EnergyPurchaseService.MakePurchase(energyId, quantity);

            // Step 4: Verify the Transaction Message
            var expectedCost = pricePerUnit * quantity; // Calculate the total cost
            var remainingQuantity = initialQuantity - quantity; // Calculate remaining quantity
            var expectedMessage = $"You have purchased {quantity} {unitType} at a cost of {expectedCost:F1} there are {remainingQuantity} units remaining.";

            // Validate the static part exists in the response
            response.Message.Should().NotBeNullOrWhiteSpace("Purchase operation should provide a message.");
            response.Message.Should().StartWith(expectedMessage, $"The response message should match the expected part. Ensure quantity, cost, and units are correct.");
        }

        [TestCase(2, 10)]  // Nuclear fuel, quantity = 10
        public async Task TestPurchaseUnavailableFuelNegativeScenario(int energyId, int quantity)
        {
            // Step 1: Reset the Energy Data
            var resetResponse = await EnergyPurchaseService.ResetEnergyPurchaseData();
            resetResponse.Message.Should().NotBeNullOrWhiteSpace("Reset operation should succeed.");
            resetResponse.Message.Should().Contain("Success");

            // Step 2: Try to purchase an unavailable fuel (Nuclear)
            var response = await EnergyPurchaseService.MakePurchase(energyId, quantity);

            // Step 3: Verify the error message in the response
            response.Message.Should().NotBeNullOrWhiteSpace("Purchase operation should provide a response message.");
            response.Message.Should().Be("There is no nuclear fuel to purchase!", "The response message should indicate nuclear fuel is unavailable.");
        }

        /// <summary>
        /// Helper method to get energy details based on energyId.
        /// </summary>
        private static EnergyDetails GetEnergyDetailsById(EnergyType energyData, int energyId)
        {
            return energyId switch
            {
                3 => energyData.Electric ?? throw new InvalidOperationException("Electric data is missing."),
                1 => energyData.Gas ?? throw new InvalidOperationException("Gas data is missing."),
                2 => energyData.Nuclear ?? throw new InvalidOperationException("Nuclear data is missing."),
                4 => energyData.Oil ?? throw new InvalidOperationException("Oil data is missing."),
                _ => throw new ArgumentOutOfRangeException(nameof(energyId), $"Energy ID {energyId} is not recognized.")
            };
        }
    }
}
