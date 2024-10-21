using System.Text.Json;
using PW.EFootballClub.PlayersAppUsvc.Models;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Implementations;

public class TimetableServices : ITimetableServices
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUri = "https://timetablesdatausv.proudwave-86bbe606.westeurope.azurecontainerapps.io/TimetablesData";

    public TimetableServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
        
    public async Task<List<Timetable>> GetAllTimetablesAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUri}/getAllTimetables");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Timetable>>(content)!;
    }
}