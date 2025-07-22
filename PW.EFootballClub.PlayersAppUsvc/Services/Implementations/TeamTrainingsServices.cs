using System.Text.Json;
using PW.EFootballClub.PlayersAppUsvc.Models;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Implementations;

public class TeamTrainingsServices : ITeamTrainingsServices
{
    private readonly HttpClient _httpClient;
    private readonly string? _baseUri = Environment.GetEnvironmentVariable("TEAM_TRAININGS_DATA_USVC_URL");

    public TeamTrainingsServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TeamTraining>> GetTeamTrainingsAsync(string apimKey)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUri}");
        request.Headers.Add("Ocp-Apim-Subscription-Key", apimKey);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<TeamTraining>>(content)!;
    }
    
    
}

