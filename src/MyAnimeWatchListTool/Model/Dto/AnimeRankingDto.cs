using MyAnimeWatchListTool.Model.Sections;

namespace MyAnimeWatchListTool.Model.Dto;

public record AnimeRankingDto(List<Data> Data, Paging Paging);