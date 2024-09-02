using System.Text.Json.Serialization;

namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class IndividualTraining
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("duration")]
    public required int Duration { get; set; }

    [JsonPropertyName("exercises")] 
    public required List<Exercise> Exercises { get; set; } = new();
    
    [JsonPropertyName("isDone")]
    public bool IsDone { get; set; }
    
    [JsonPropertyName("playerId")]
    public string? PlayerId { get; set; }
    
    [JsonPropertyName("coachId")]
    public string? CoachId { get; set; }
}