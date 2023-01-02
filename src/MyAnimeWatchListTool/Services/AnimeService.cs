using Microsoft.Extensions.Logging;
using MyAnimeWatchListTool.Extensions;
using MyAnimeWatchListTool.Model;
using MyAnimeWatchListTool.Model.Enum;
using MyAnimeWatchListTool.Repositories.Interfaces;
using MyAnimeWatchListTool.Services.Interfaces;

namespace MyAnimeWatchListTool.Services;

public class AnimeService : IAnimeService
{
    private readonly IMyAnimeListService _myAnimeListService;
    private readonly IAnimeDetailsRepository _animeDetailsRepository;
    private readonly ISeasonalAnimeRepository _seasonalAnimeRepository;
    private readonly ISuggestedAnimeRepository _suggestedAnimeRepository;
    private readonly IAnimeRankingRepository _animeRankingRepository;
    private readonly ILogger<AnimeService> _logger;

    public AnimeService(IMyAnimeListService myAnimeListService, IAnimeDetailsRepository animeDetailsRepository,
        ISeasonalAnimeRepository seasonalAnimeRepository, ISuggestedAnimeRepository suggestedAnimeRepository,
        IAnimeRankingRepository animeRankingRepository, ILogger<AnimeService> logger)
    {
        _myAnimeListService = myAnimeListService;
        _animeDetailsRepository = animeDetailsRepository;
        _seasonalAnimeRepository = seasonalAnimeRepository;
        _suggestedAnimeRepository = suggestedAnimeRepository;
        _animeRankingRepository = animeRankingRepository;
        _logger = logger;
    }

    public async Task GetAnimeDetails(string token, int animeId, CancellationToken cancellationToken)
    {
        try
        {
            var animeDetailsDto = await _myAnimeListService.GetAnimeDetails(token, animeId, cancellationToken);

            if (animeDetailsDto is not null)
            {
                await _animeDetailsRepository.AddAnimeDetails(animeDetailsDto.ToAnimeDetails());
            }
            else
            {
                _logger.LogError("RequestedAnime Details is null.");
            }
        }
        catch (Exception e)
        {
            _logger.LogError("AddAnimeDetails {Message}", e.StackTrace);
        }
    }

    public async Task GetSuggestedAnime(string token, int count, CancellationToken cancellationToken)
    {
        try
        {
            var suggestedAnimeDto = await _myAnimeListService.GetSuggestedAnime(token, cancellationToken, count);

            if (suggestedAnimeDto is not null)
            {
                foreach (var animeData in suggestedAnimeDto.Data)
                {
                    await _suggestedAnimeRepository.AddSuggestedAnime(animeData.Node.ToSuggestedAnime());
                    _logger.LogInformation("Add Anime {Name} with {Id}", animeData.Node.Title, animeData.Node.Id);
                }
            }
            else
            {
                _logger.LogError("Requested Suggested Anime is null.");
            }
        }
        catch (Exception e)
        {
            _logger.LogError("AddSuggestedAnime {Message}", e.StackTrace);
        }
    }

    public async Task GetSeasonalAnimes(string token, int year, Season season, int count, CancellationToken cancellationToken)
    {
        try
        {
            var seasonalAnimesDto = await _myAnimeListService.GetSeasonalAnimes(token, year, season, cancellationToken, count);

            if (seasonalAnimesDto is not null)
            {
                foreach (var seasonalAnime in seasonalAnimesDto.Data.Select(animeData => new SeasonalAnime
                         {
                             Id = animeData.Node.Id,
                             Title = animeData.Node.Title,
                             MainPicture = animeData.Node.MainPicture,
                             Year = seasonalAnimesDto.Season.Year,
                             Season = seasonalAnimesDto.Season.Season,
                         }))
                {
                    await _seasonalAnimeRepository.AddSeasonalAnime(seasonalAnime);
                    _logger.LogInformation("Add Anime {Name} with {Id}", seasonalAnime.Title, seasonalAnime.Id);
                }
            }
            else
            {
                _logger.LogError("Requested Seasonal Animes is null.");
            }
        }
        catch (Exception e)
        {
            _logger.LogError("AddSeasonalAnimes {Message}", e.StackTrace);
        }
    }

    public async Task GetAnimeRanking(string token, CancellationToken cancellationToken, int count)
    {
        try
        {
            var rankingAnimesDto = await _myAnimeListService.GetAnimeRanking(token, RankingType.All, cancellationToken, count);

            if (rankingAnimesDto is not null)
            {
                foreach (var animeData in rankingAnimesDto.Data)
                {
                    await _animeRankingRepository.AddAnime(animeData.Node.ToAnimeRanking());
                    _logger.LogInformation("Add Anime {Name} with {Id}", animeData.Node.Title, animeData.Node.Id);
                }
            }
            else
            {
                _logger.LogError("Requested Anime Ranking is null.");
            }
        }
        catch (Exception e)
        {
            _logger.LogError("AddAnimeRanking {Message}", e.StackTrace);
        }
    }
}