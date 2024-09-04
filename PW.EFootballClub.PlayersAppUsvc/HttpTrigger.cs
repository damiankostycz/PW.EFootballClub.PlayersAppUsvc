using System.Net;
using System.Text.Json;
using Azure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PW.EFootballClub.PlayersAppUsvc.Models;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

namespace PW.EFootballClub.PlayersAppUsvc;

public class HttpTrigger
{
    private readonly ILogger _logger;
    private readonly IMatchServices _matchService;
    private readonly IIndividualTrainingsServices _individualTrainingsServices;
    private readonly ITeamTrainingsServices _teamTrainingsServices;
    public HttpTrigger(ILoggerFactory loggerFactory, IMatchServices matchService, IIndividualTrainingsServices individualTrainingsServices, ITeamTrainingsServices teamTrainingsServices)
    {
        _logger = loggerFactory.CreateLogger<HttpTrigger>();
        _matchService = matchService;
        _individualTrainingsServices = individualTrainingsServices;
        _teamTrainingsServices = teamTrainingsServices;
    }

    [Function("GetAllMatches")]
    public async Task<HttpResponseData> GetAllMatches(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getAllMatches")] HttpRequestData req)
    {
        try
        {
            _logger.LogInformation("Fetching all matches...");
            var matches = await _matchService.GetAllMatchesAsync();

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


    
    [Function("GetMatchById")]
    public async Task<HttpResponseData> GetMatchById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getMatch")] HttpRequestData req)
    {
        try
        {
            var queryParameters = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var matchId = queryParameters["id"];

            if (string.IsNullOrEmpty(matchId))
            {
                _logger.LogWarning("Bad request: Missing match ID.");
                var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequestResponse.WriteStringAsync("Bad request: Missing match ID.");
                return badRequestResponse;
            }

            _logger.LogInformation($"Fetching match with ID: {matchId}...");
            Match match = await _matchService.GetMatchByIdAsync(matchId);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(match);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching match: {ex.Message}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("An error occurred while fetching the match data.");
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
                var queryParameters = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
                var individualTrainingId = queryParameters["id"];
                if (string.IsNullOrEmpty(individualTrainingId))
                {
                    _logger.LogInformation("Fetching all team trainings...");
                    var individualTrainings = await _individualTrainingsServices.GetIndividualTrainingsAsync();
                    response = req.CreateResponse(HttpStatusCode.OK);
                    await response.WriteAsJsonAsync(individualTrainings);
                }
                else
                {
                    _logger.LogInformation($"Fetching individual training with ID: {individualTrainingId}...");
                    var individualTraining = await _individualTrainingsServices.GetIndividualTrainingByIdAsync(individualTrainingId);
                    response = req.CreateResponse(HttpStatusCode.OK);
                    await response.WriteAsJsonAsync(individualTraining);
                }
                break;

            case "PUT":
                _logger.LogInformation("Handling PUT request to update individual training...");
                var putBody = await req.ReadAsStringAsync();
                var updatedIndividualTraining = JsonSerializer.Deserialize<IndividualTraining>(putBody!);

                if (updatedIndividualTraining != null)
                {
                    try
                    {
                        var result = await _individualTrainingsServices.UpdateIndividualTrainingAsync(updatedIndividualTraining);
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
            var teamTrainings = await _teamTrainingsServices.GetTeamTrainingsAsync();

            if (!string.IsNullOrEmpty(teamId) && teamTrainings.Count != 0)
            {
                teamTrainings = teamTrainings.Where(t => t.TeamId == teamId).ToList();
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
    
    
    [Function("HttpTrigger")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return response;
        
    }
    
}