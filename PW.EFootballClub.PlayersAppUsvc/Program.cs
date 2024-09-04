using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PW.EFootballClub.PlayersAppUsvc.Services.Implementations;
using PW.EFootballClub.PlayersAppUsvc.Services.Interfaces;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddHttpClient<IMatchServices, MatchServices>();
        services.AddHttpClient<ITeamTrainingsServices, TeamTrainingsServices>();
        services.AddHttpClient<IIndividualTrainingsServices, IndividualTrainingsServices>();

    })
    .Build();

host.Run();
