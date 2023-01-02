using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using MyAnimeWatchListTool.Configuration;
using MyAnimeWatchListTool.Model.Dto;
using MyAnimeWatchListTool.Model.Enum;
using MyAnimeWatchListTool.Services.Interfaces;
using MyAnimeWatchListTool.Utils;

namespace MyAnimeWatchListTool.Services;

public class MyAnimeListService : IMyAnimeListService
{
    private readonly HttpClient _httpClient;
    private readonly Config _config;
    private string _codeVerifier = string.Empty;

    public MyAnimeListService(HttpClient httpClient, Config config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public string GenerateAuthorizeRequest()
    {
        var codeVerifier = Pkce.GenerateCodeVerifier();
        var codeChallenge = Pkce.GenerateCodeChallenge(codeVerifier);
        _codeVerifier = codeChallenge;

        return $"{UrlConstants.BaseAuthUrl}&client_id={_config.ClientId}&code_challenge={codeChallenge}";
    }

    public async Task<TokenDto?> SendGenerateTokenRequest(string code, CancellationToken cancellationToken)
    {
        var payLoad = new Dictionary<string, string>
        {
            {"client_id", $"{_config.ClientId}"},
            {"client_secret", $"{_config.ClientSecret}"},
            {"code", $"{code}"},
            {"code_verifier", $"{_codeVerifier}"},
            {"grant_type", "authorization_code"},
        };

        var content = new FormUrlEncodedContent(payLoad);

        var responseMessage = await _httpClient.PostAsync(UrlConstants.GenerateTokenUrl, content, cancellationToken);

        return await responseMessage.Content.ReadFromJsonAsync<TokenDto>(GetJsonSerializerOptions(), cancellationToken);
    }

    public async Task<SeasonalAnimesDto?> GetSeasonalAnimes(string token, int year, Season season, CancellationToken cancellationToken, int count = 2)
    {
        SetRequestHeader(token);
        var url = $"{UrlConstants.GetSeasonalAnimeUrl}{year}/{season.ToLower()}?limit={count}";
        return await _httpClient.GetFromJsonAsync<SeasonalAnimesDto>(url, GetJsonSerializerOptions(), cancellationToken);
    }

    public async Task<AnimeRankingDto?> GetAnimeRanking(string token, RankingType rankingType, CancellationToken cancellationToken, int count = 2)
    {
        SetRequestHeader(token);
        var url = $"{UrlConstants.GetAnimeRankingUrl}?ranking_type={rankingType.ToLower()}&limit={count}";
        return await _httpClient.GetFromJsonAsync<AnimeRankingDto>(url, GetJsonSerializerOptions(), cancellationToken);
    }

    public async Task<AnimeDetailsDto?> GetAnimeDetails(string token, int animeId, CancellationToken cancellationToken)
    {
        SetRequestHeader(token);
        var url = $"{UrlConstants.GetAnimeDetailsUrl}{animeId}?fields=id,title,main_picture,mean,rank,popularity,num_list_users,num_scoring_users," +
                  $"created_at,updated_at,media_type,status,genres,num_episodes,average_episode_duration,rating,studios,statistics";
        return await _httpClient.GetFromJsonAsync<AnimeDetailsDto>(url, GetJsonSerializerOptions(), cancellationToken);
    }

    public async Task<SuggestedAnimeDto?> GetSuggestedAnime(string token, CancellationToken cancellationToken, int count = 2)
    {
        SetRequestHeader(token);
        var url = $"{UrlConstants.GetSuggestedAnimeUrl}?limit={count}";
        return await _httpClient.GetFromJsonAsync<SuggestedAnimeDto>(url, GetJsonSerializerOptions(), cancellationToken);
    }

    private void SetRequestHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
        };
    }
}