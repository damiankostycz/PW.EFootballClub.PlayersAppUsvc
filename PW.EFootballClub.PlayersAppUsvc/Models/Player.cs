namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class Player
{
    public string PlayerId { get; set; } = string.Empty;
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string Sex { get; set; }
    public required string Position { get; set; }
    public required PlayerStats PlayerStats { get; set; }
    public string? TeamId { get; set; }

}