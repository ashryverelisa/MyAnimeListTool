using MyAnimeWatchListTool.Model.Sections;

namespace MyAnimeWatchListTool.Model.Dto;

public record SeasonalAnimesDto(List<Data> Data, Paging Paging, SeasonResponse Season);