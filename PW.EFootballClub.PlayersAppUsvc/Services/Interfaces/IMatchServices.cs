using PW.EFootballClub.PlayersAppUsvc.Models;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

public interface IMatchServices
{
    Task<List<Match>> GetAllMatchesAsync();
    Task<Match> GetMatchByIdAsync(string id);
}