using System.Text;
using System.Text.Json;
using PW.EFootballClub.PlayersAppUsvc.Models;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

namespace PW.EFootballClub.PlayersAppUsvc.Services.Implementations;

public class IndividualTrainingsServices : IIndividualTrainingsServices
{ 
   private readonly HttpClient _httpClient;
   private readonly string? _baseUri = Environment.GetEnvironmentVariable("INDIVIDUAL_TRAININGS_DATA_USVC_URL");
    
    

    public IndividualTrainingsServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<IndividualTraining>> GetIndividualTrainingsAsync(string apimKey)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUri}");
        request.Headers.Add("Ocp-Apim-Subscription-Key", apimKey);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<IndividualTraining>>(content)!;
    }


    public async Task<IndividualTraining> GetIndividualTrainingByIdAsync(string id, string apimKey)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUri}/{id}");
        request.Headers.Add("Ocp-Apim-Subscription-Key", apimKey);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IndividualTraining>(content)!;
    }
    public async Task<IndividualTraining> UpdateIndividualTrainingAsync(IndividualTraining teamTraining, string apimKey)
    {
        var json = JsonSerializer.Serialize(teamTraining);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUri}/{teamTraining.Id}");
        request.Content = content;
        request.Headers.Add("Ocp-Apim-Subscription-Key", apimKey);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IndividualTraining>(responseContent)!;
    }

    public async Task<IndividualTraining> AddIndividualTrainingAsync(IndividualTraining teamTraining, string apimKey)
    {
        var json = JsonSerializer.Serialize(teamTraining);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUri}");
        request.Content = content;
        request.Headers.Add("Ocp-Apim-Subscription-Key", apimKey);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IndividualTraining>(responseContent)!;
    }
}