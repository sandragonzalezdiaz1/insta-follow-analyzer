using System.Text.Json.Serialization;

namespace InstaFollowAnalyzer.Models
{
    public class InstaStringData
    {
        [JsonPropertyName("href")]
        public string Href { get; set; } = "";

        [JsonPropertyName("value")]
        public string Value { get; set; } = "";

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

    }
}
