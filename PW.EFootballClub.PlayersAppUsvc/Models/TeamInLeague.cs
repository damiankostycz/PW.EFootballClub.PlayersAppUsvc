namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class TeamInLeague
{
    public required string TeamName { get; set; }
    public int MatchesPlayed { get; set; }
    public int Points { get; set; }
}