namespace MyAnimeWatchListTool.Utils;

public static class UrlConstants
{
    public static readonly string BaseAuthUrl = "https://myanimelist.net/v1/oauth2/authorize?response_type=code";
    public static readonly string GenerateTokenUrl = "https://myanimelist.net/v1/oauth2/token";
    public static readonly string GetSeasonalAnimeUrl = "https://api.myanimelist.net/v2/anime/season/";
    public static readonly string GetAnimeDetailsUrl = "https://api.myanimelist.net/v2/anime/";
    public static readonly string GetAnimeRankingUrl = "https://api.myanimelist.net/v2/anime/ranking";
    public static readonly string GetSuggestedAnimeUrl = "https://api.myanimelist.net/v2/anime/suggestions";
}