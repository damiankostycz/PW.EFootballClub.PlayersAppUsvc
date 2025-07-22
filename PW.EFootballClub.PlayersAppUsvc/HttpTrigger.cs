using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PW.EFootballClub.PlayersAppUsvc.Models;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

namespace PW.EFootballClub.PlayersAppUsvc;

public class HttpTrigger
{
    private readonly ILogger _logger;
    private readonly IMatchServices _matchServices;
    private readonly IIndividualTrainingsServices _individualTrainingsServices;
    private readonly ITeamTrainingsServices _teamTrainingsServices;
    private readonly ITimetableServices _timetableService;
    private readonly string? _apimKey = Environment.GetEnvironmentVariable("APIM_KEY");

    public HttpTrigger(ILoggerFactory loggerFactory, ITimetableServices timetableServices,IMatchServices matchService, IIndividualTrainingsServices individualTrainingsServices, ITeamTrainingsServices teamTrainingsServices)
    {
        _logger = loggerFactory.CreateLogger<HttpTrigger>();
        _matchServices = matchService;
        _individualTrainingsServices = individualTrainingsServices;
        _teamTrainingsServices = teamTrainingsServices;
        _timetableService = timetableServices;
    }

    [Function("GetAllMatches")]
    public async Task<HttpResponseData> GetAllMatches(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getAllMatches")] HttpRequestData req)
    {
        try
        {
            _logger.LogInformation("Fetching all matches...");
            var matches = await _matchServices.GetAllMatchesAsync(_apimKey);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(matches);
            _logger.LogInformation("Received data from API...");
            _logger.LogInformation($"Fetched {matches.Count} matches");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching matches: {ex.Message}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("An error occurred while fetching match data.");
            return errorResponse;
        }
    }

    
    
     [Function("HandleIndividualTrainings")]
    public async Task<HttpResponseData> HandleTeamTraining(
        [HttpTrigger(AuthorizationLevel.Function, "get", "put", Route = "handleIndividualTrainings")] HttpRequestData req)
    {
        HttpResponseData response;

        switch (req.Method.ToUpper())
        {
            case "GET":

                _logger.LogInformation("Fetching all team trainings...");
                var individualTrainings = await _individualTrainingsServices.GetIndividualTrainingsAsync(_apimKey);
                response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(individualTrainings);

                break;

            case "PUT":
                _logger.LogInformation("Handling PUT request to update individual training...");
                var putBody = await req.ReadAsStringAsync();
                var updatedIndividualTraining = JsonSerializer.Deserialize<IndividualTraining>(putBody!);

                if (updatedIndividualTraining != null)
                {
                    try
                    {
                        var result = await _individualTrainingsServices.UpdateIndividualTrainingAsync(updatedIndividualTraining, _apimKey);
                        response = req.CreateResponse(HttpStatusCode.OK);
                        _logger.LogError($"Updated training with id: {updatedIndividualTraining.Id}");
                        await response.WriteAsJsonAsync(result);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Failed to update training: {ex.Message}");
                        response = req.CreateResponse(HttpStatusCode.InternalServerError);
                        await response.WriteStringAsync($"Failed to update training. Error: {ex.Message}");
                    }
                }
                else
                {
                    response = req.CreateResponse(HttpStatusCode.BadRequest);
                    await response.WriteStringAsync("Invalid training data.");
                }
                break;

            default:
                response = req.CreateResponse(HttpStatusCode.MethodNotAllowed);
                await response.WriteStringAsync("Method not allowed.");
                break;
        }

        return response;
    }


    [Function("GetTeamTrainings")]
    public async Task<HttpResponseData> GetTeamTrainings(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getTeamTrainings")] HttpRequestData req,
        FunctionContext executionContext)
    {
        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

        try
        {
            var queryParameters = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var teamId = queryParameters["teamId"];
            var teamTrainings = await _teamTrainingsServices.GetTeamTrainingsAsync(_apimKey);

            if (!string.IsNullOrEmpty(teamId) && teamTrainings.Count != 0)
            {
                teamTrainings = teamTrainings.Where(t => t.TeamID == teamId).ToList();
                await response.WriteAsJsonAsync(teamTrainings);
            }
            else
            {
                await response.WriteAsJsonAsync(teamTrainings);
            }

        }
        catch (Exception exception)
        {
            _logger.LogError($"An error occurred: {exception.Message}");
            response = req.CreateResponse(HttpStatusCode.InternalServerError);
            await response.WriteStringAsync($"An error occurred while processing the request: {exception.Message}");
        }

        return response;
    }
    
    [Function("GetAllTimetables")]
    public async Task<HttpResponseData> GetAllTimetables(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getAllTimetables")] HttpRequestData req)
    {
        _logger.LogInformation("Handling request: {Method}", req.Method);

        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

        try
        {
            var matchesTask = _matchServices.GetAllMatchesAsync(_apimKey);
            var timetablesTask = _timetableService.GetAllTimetablesAsync(_apimKey);

            await Task.WhenAll(matchesTask, timetablesTask);

            var timetables = await timetablesTask;
            var matches = await matchesTask;

            var matchDictionary = matches.ToDictionary(m => m.Id);

            var timetableDtos = timetables.Select(timetable => new TimetableDto
            {
                Id = timetable.Id,
                Team = timetable.Team,
                League = timetable.League,
                Season = timetable.Season,
                Matches = timetable.Matches?
                    .Select(matchId => matchDictionary.TryGetValue(matchId, out var match) ? match : null)
                    .Where(match => match != null)
                    .ToList()!
            }).ToList();

            await response.WriteAsJsonAsync(timetableDtos);

            _logger.LogInformation("Received timetable data from API...");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching timetables.");
            response = req.CreateResponse(HttpStatusCode.InternalServerError);
            await response.WriteStringAsync("An error occurred while fetching timetables.");
        }

        return response;
    }

    
}