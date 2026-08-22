using System.Text.Json.Serialization;

namespace InstaFollowAnalyzer.Models
{
    public class FollowingExport
    {
        [JsonPropertyName("relationships_following")]
        public List<FollowingItem> RelationshipsFollowing { get; set; } = [];
    }
}
