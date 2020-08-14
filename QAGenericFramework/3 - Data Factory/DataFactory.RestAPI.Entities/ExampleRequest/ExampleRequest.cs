using System.Text.Json.Serialization;

namespace DataFactory.RestAPI.Entities.ExampleRequest
{
    public class ExampleRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("requestPropertyOne")]
        public string RequestPropertyOne { get; set; }

        [JsonPropertyName("requestPropertyTwo")]
        public string RequestPropertyTwo { get; set; }
    }
}