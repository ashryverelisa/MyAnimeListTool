using MyAnimeWatchListTool.Model.Sections;

namespace MyAnimeWatchListTool.Model.Dto;

public record AnimeDetailsDto(int Id, string Title, Picture MainPicture, DateTime StartDate, DateTime EndDate, float Mean, int Rank, int Popularity,
    int NumListUsers, int NumScoringUsers, string MediaType, string Status, List<Genre> Genres, int NumEpisodes, int AverageEpisodeDuration, string Rating,
    List<Studio> Studios, Statistics Statistics);