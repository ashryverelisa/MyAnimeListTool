using MyAnimeWatchListTool.Model.Dto;
using MyAnimeWatchListTool.Model.Enum;

namespace MyAnimeWatchListTool.Services.Interfaces;

public interface IMyAnimeListService
{
    string GenerateAuthorizeRequest();
    Task<TokenDto?> SendGenerateTokenRequest(string code, CancellationToken cancellationToken);
    Task<SeasonalAnimesDto?> GetSeasonalAnimes(string token, int year, Season season, CancellationToken cancellationToken, int count = 100);
    Task<AnimeRankingDto?> GetAnimeRanking(string token, RankingType rankingType, CancellationToken cancellationToken, int count = 100);
    Task<AnimeDetailsDto?> GetAnimeDetails(string token, int animeId, CancellationToken cancellationToken);
    Task<SuggestedAnimeDto?> GetSuggestedAnime(string token, CancellationToken cancellationToken, int count = 100);
}