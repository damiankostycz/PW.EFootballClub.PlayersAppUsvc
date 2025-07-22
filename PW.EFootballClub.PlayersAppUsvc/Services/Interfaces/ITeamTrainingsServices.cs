using PW.EFootballClub.PlayersAppUsvc.Models;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

public interface ITeamTrainingsServices
{
    Task<List<TeamTraining>> GetTeamTrainingsAsync(string apimKey);
}