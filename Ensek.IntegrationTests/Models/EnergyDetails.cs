using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ensek.IntegrationTests.Models
{
    public class EnergyDetails
    {
        [JsonPropertyName("energy_id")]
        public int EnergyId { get; set; }

        [JsonPropertyName("price_per_unit")]
        public double PricePerUnit { get; set; }

        [JsonPropertyName("quantity_of_units")]
        public int QuantityOfUnits { get; set; }

        [JsonPropertyName("unit_type")]
        public string? UnitType { get; set; }
    }
}
