using System.Text.Json.Serialization;

namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class TeamTraining
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("teamID")]
    public string? TeamId { get; set; }
}