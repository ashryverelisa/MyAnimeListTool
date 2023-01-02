using MyAnimeWatchListTool.Model.Enum;

namespace MyAnimeWatchListTool.Services.Interfaces;

public interface IAnimeService
{
    Task GetAnimeDetails(string token, int animeId, CancellationToken cancellationToken);
    Task GetSuggestedAnime(string token, int count, CancellationToken cancellationToken);
    Task GetSeasonalAnimes(string token, int year, Season season, int count, CancellationToken cancellationToken);
    Task GetAnimeRanking(string token, CancellationToken cancellationToken, int count);
}