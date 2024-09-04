using System.Text;
using System.Text.Json;
using PW.EFootballClub.PlayersAppUsvc.Models;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Implementations;

public class IndividualTrainingsServices : IIndividualTrainingsServices
{ 
    private readonly HttpClient _httpClient;
    private readonly string _baseUri = "https://individualtrainingdatausvc.proudwave-86bbe606.westeurope.azurecontainerapps.io/IndividualTrainingData";

    public IndividualTrainingsServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<IndividualTraining>> GetIndividualTrainingsAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUri}/getAllTrainings");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<IndividualTraining>>(content)!;
    }

    public async Task<IndividualTraining> GetIndividualTrainingByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"{_baseUri}/getTrainingById?trainingId={id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IndividualTraining>(content)!;
    }
    
    public async Task<IndividualTraining> UpdateIndividualTrainingAsync(IndividualTraining teamTraining)
    {
        var json = JsonSerializer.Serialize(teamTraining);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_baseUri}/modifyTraining", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IndividualTraining>(responseContent)!;
    }
}