﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ensek.IntegrationTests.Models
{
    public class MessageResponse
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
