using System.Text.Json.Serialization;

namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class Timetable
{
    
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("team")]
    public string Team { get; set; } = string.Empty;

    [JsonPropertyName("league")]
    public string League { get; set; } = string.Empty;

    [JsonPropertyName("matches")]
    public List<string>? Matches { get; set; }

    [JsonPropertyName("season")]
    public string Season { get; set; } = string.Empty;
}