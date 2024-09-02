using System.Text.Json.Serialization;

namespace PW.EFootballClub.PlayersAppUsvc.Models
{
    public class Lineup
    {
        [JsonPropertyName("formation")]
        public string? Formation { get; set; } = string.Empty;

        [JsonPropertyName("goalkeeper")]
        public int Goalkeeper { get; set; }

        [JsonPropertyName("defenders")]
        public List<int>? Defenders { get; set; } = new List<int>();

        [JsonPropertyName("midfielders")]
        public List<int>? Midfielders { get; set; } = new List<int>();

        [JsonPropertyName("strikers")]
        public List<int>? Strikers { get; set; } = new List<int>();

        [JsonPropertyName("substitutes")]
        public List<int>? Substitutes { get; set; } = new List<int>();
    }
}
