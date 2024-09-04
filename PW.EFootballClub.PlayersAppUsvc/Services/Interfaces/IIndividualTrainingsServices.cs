using PW.EFootballClub.PlayersAppUsvc.Models;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

public interface IIndividualTrainingsServices
{
    Task<List<IndividualTraining>> GetIndividualTrainingsAsync();
    Task<IndividualTraining> GetIndividualTrainingByIdAsync(string id);
    Task<IndividualTraining> UpdateIndividualTrainingAsync(IndividualTraining individualTraining);

}