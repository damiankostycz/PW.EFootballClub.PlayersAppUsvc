using System.Text.Json;
using PW.EFootballClub.PlayersAppUsvc.Models;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Implementations;

public class TeamTrainingsServices : ITeamTrainingsServices
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUri = "https://teamtrainingsdatausvc.proudwave-86bbe606.westeurope.azurecontainerapps.io";

    public TeamTrainingsServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TeamTraining>> GetTeamTrainingsAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUri}/TeamTrainingsData/getAllTeamTrainings");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<TeamTraining>>(content)!;
    }

    public async Task<TeamTraining> GetTeamTrainingByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"{_baseUri}/TeamTrainingsData/getTeamTrainingById?id={id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TeamTraining>(content)!;
    }
    
}

