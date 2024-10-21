namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class TimetableDto
{
    public string Id { get; set; } = string.Empty;
    public string Team { get; set; } = string.Empty;
    public string League { get; set; } = string.Empty;
    public List<Match>? Matches { get; set; }
    public string Season { get; set; } = string.Empty;
}