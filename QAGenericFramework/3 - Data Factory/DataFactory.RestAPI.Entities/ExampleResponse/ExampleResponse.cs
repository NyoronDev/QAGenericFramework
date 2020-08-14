using System.Text.Json.Serialization;

namespace DataFactory.RestAPI.Entities.ExampleResponse
{
    public class ExampleResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("responsePropertyOne")]
        public string ResponsePropertyOne { get; set; }

        [JsonPropertyName("responsePropertyTwo")]
        public string ResponsePropertyTwo { get; set; }
    }
}