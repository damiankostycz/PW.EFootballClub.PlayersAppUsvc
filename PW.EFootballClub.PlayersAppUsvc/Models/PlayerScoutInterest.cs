namespace PW.EFootballClub.PlayersAppUsvc.Models;

public class PlayerScoutInterest
{
    public required Player Player { get; set; }
    public List<Scout> InterestedScouts { get; set; } = new List<Scout>();
}