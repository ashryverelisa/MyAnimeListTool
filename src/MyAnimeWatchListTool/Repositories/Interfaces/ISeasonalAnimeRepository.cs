using MyAnimeWatchListTool.Model;

namespace MyAnimeWatchListTool.Repositories.Interfaces;

public interface ISeasonalAnimeRepository
{
    Task AddSeasonalAnime(SeasonalAnime seasonalAnime);
}