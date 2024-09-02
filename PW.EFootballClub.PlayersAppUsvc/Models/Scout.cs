using System.Text.Json.Serialization;

namespace PW.EFootballClub.PlayersAppUsvc.Models
{
    public class Scout
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("lastName")]
        public required string LastName { get; set; }

        [JsonPropertyName("country")]
        public required string Country { get; set; }

        [JsonPropertyName("club")]
        public required string Club { get; set; }

        [JsonPropertyName("languages")]
        public required List<string> Languages { get; set; }

        [JsonPropertyName("playersUnderObservation")]
        public required List<string> PlayersUnderObservation { get; set; }
    }
}
