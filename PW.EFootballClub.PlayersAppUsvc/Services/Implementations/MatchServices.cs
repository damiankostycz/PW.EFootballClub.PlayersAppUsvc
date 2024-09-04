using System.Text.Json;
using PW.EFootballClub.PlayersAppUsvc.Models;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Implementations;

public class MatchServices : IMatchServices
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUri = "https://matchesdatausvc.proudwave-86bbe606.westeurope.azurecontainerapps.io/MatchesData";

    public MatchServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Match>> GetAllMatchesAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUri}/getAllMatches");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Match>>(content)!;
    }

    public async Task<Match> GetMatchByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"{_baseUri}/getMatchById?matchId={id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Match>(content)!;
    }
}