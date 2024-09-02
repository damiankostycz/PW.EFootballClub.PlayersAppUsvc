namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class Team
{
    public string TeamId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<TeamInLeague>? League { get; set; }
}