using MyAnimeWatchListTool.Model;

namespace MyAnimeWatchListTool.Repositories.Interfaces;

public interface IAnimeDetailsRepository
{
    Task AddAnimeDetails(AnimeDetails animeDetails);
}