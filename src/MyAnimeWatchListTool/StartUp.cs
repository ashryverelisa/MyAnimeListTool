using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyAnimeWatchListTool.Model.Enum;
using MyAnimeWatchListTool.Services.Interfaces;

namespace MyAnimeWatchListTool;

public class StartUp : IHostedService
{
    private readonly IMyAnimeListService _myAnimeListService;
    private readonly IAnimeService _animeService;
    private readonly ILogger<StartUp> _logger;

    public StartUp(IMyAnimeListService myAnimeListService, IAnimeService animeService, ILogger<StartUp> logger)
    {
        _myAnimeListService = myAnimeListService;
        _animeService = animeService;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var authoriseUrl = _myAnimeListService.GenerateAuthorizeRequest();
        _logger.LogInformation("Please open this {Url} and press Allow.", authoriseUrl);

        _logger.LogInformation("Please enter the code from the redirect URL.");
        var code = Console.ReadLine()?.TrimEnd();

        if (!string.IsNullOrWhiteSpace(code))
        {
            var requestToken = await _myAnimeListService.SendGenerateTokenRequest(code, cancellationToken);

            if (requestToken is not null)
            {
                await _animeService.GetAnimeRanking(requestToken.AccessToken, cancellationToken, 100);
                await _animeService.GetAnimeDetails(requestToken.AccessToken, 1000, cancellationToken);
                await _animeService.GetSeasonalAnimes(requestToken.AccessToken, 2022, Season.Fall, 100, cancellationToken);
                await _animeService.GetSuggestedAnime(requestToken.AccessToken, 100, cancellationToken);
                _logger.LogInformation("Finish!");
            }
            else
            {
                _logger.LogWarning("Something went wrong while generating the token. Please try again.");
            }
        }
        else
        {
            _logger.LogWarning("Please enter a valid input.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}