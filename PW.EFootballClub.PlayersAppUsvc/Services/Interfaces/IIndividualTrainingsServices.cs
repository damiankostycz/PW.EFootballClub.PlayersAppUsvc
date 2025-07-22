using PW.EFootballClub.PlayersAppUsvc.Models;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

public interface IIndividualTrainingsServices
{
    Task<List<IndividualTraining>> GetIndividualTrainingsAsync(string apimKey);
    Task<IndividualTraining> UpdateIndividualTrainingAsync(IndividualTraining individualTraining, string apimKey);

}