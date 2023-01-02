using MyAnimeWatchListTool.Model;

namespace MyAnimeWatchListTool.Repositories.Interfaces;

public interface IAnimeRankingRepository
{
    Task AddAnime(AnimeRanking animeRanking);
}