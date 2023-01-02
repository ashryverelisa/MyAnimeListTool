using MyAnimeWatchListTool.Model.Sections;

namespace MyAnimeWatchListTool.Model.Dto;

public record SuggestedAnimeDto(List<Data> Data, Paging Paging);