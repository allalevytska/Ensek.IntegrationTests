using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ensek.IntegrationTests.Models
{
    public class EnergyType
    {
        [JsonPropertyName("electric")]
        public EnergyDetails? Electric { get; set; }

        [JsonPropertyName("gas")]
        public EnergyDetails? Gas { get; set; }

        [JsonPropertyName("nuclear")]
        public EnergyDetails? Nuclear { get; set; }

        [JsonPropertyName("oil")]
        public EnergyDetails? Oil { get; set; }
    }
}
