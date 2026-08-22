using System.Text.Json.Serialization;

namespace InstaFollowAnalyzer.Models
{
    public class FollowerItem
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("media_list_data")]
        public List<object> MediaListData { get; set; } = [];

        [JsonPropertyName("string_list_data")]
        public List<InstaStringData> StringListData { get; set; } = [];
    }
}
