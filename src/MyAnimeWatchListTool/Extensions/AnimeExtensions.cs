using MyAnimeWatchListTool.Model;
using MyAnimeWatchListTool.Model.Dto;
using MyAnimeWatchListTool.Model.Sections;

namespace MyAnimeWatchListTool.Extensions;

public static class AnimeExtensions
{
    public static AnimeRanking ToAnimeRanking(this Node node)
    {
        return new AnimeRanking
        {
            Id = node.Id,
            Title = node.Title,
            MainPicture = node.MainPicture
        };
    }

    public static SuggestedAnime ToSuggestedAnime(this Node node)
    {
        return new SuggestedAnime
        {
            Id = node.Id,
            Title = node.Title,
            MainPicture = node.MainPicture
        };
    }

    public static AnimeDetails ToAnimeDetails(this AnimeDetailsDto animeDetailsDto)
    {
        return new AnimeDetails
        {
            Id = animeDetailsDto.Id,
            Title = animeDetailsDto.Title,
            Pictures = animeDetailsDto.MainPicture,
            StartDate = animeDetailsDto.StartDate,
            EndDate = animeDetailsDto.EndDate,
            Mean = animeDetailsDto.Mean,
            Rank = animeDetailsDto.Rank,
            Popularity = animeDetailsDto.Popularity,
            NumListUsers = animeDetailsDto.NumListUsers,
            NumScoringUsers = animeDetailsDto.NumScoringUsers,
            MediaType = animeDetailsDto.MediaType,
            Status = animeDetailsDto.Status,
            Genres = animeDetailsDto.Genres,
            NumEpisodes = animeDetailsDto.NumEpisodes,
            AverageEpisodeDuration = animeDetailsDto.AverageEpisodeDuration,
            Rating = animeDetailsDto.Rating,
            Studios = animeDetailsDto.Studios,
            Statistics = animeDetailsDto.Statistics
        };
    }
}