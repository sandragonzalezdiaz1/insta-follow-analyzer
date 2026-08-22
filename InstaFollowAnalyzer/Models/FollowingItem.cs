using System.Text.Json.Serialization;

namespace InstaFollowAnalyzer.Models
{
    public class FollowingItem
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("string_list_data")]
        public List<InstaStringData> StringListData { get; set; } = [];
    }
}
