using System.Text.Json;
using PW.EFootballClub.PlayersAppUsvc.Models;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Implementations;

public class TimetableServices : ITimetableServices
{
    private readonly HttpClient _httpClient;
    private readonly string? _baseUri = Environment.GetEnvironmentVariable("TIMETABLE_DATA_USVC_URL");

    public TimetableServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
        
    public async Task<List<Timetable>> GetAllTimetablesAsync(string apimKey)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUri}");
        request.Headers.Add("Ocp-Apim-Subscription-Key", apimKey);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Timetable>>(content)!;
    }
}