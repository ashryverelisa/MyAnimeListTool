using MyAnimeWatchListTool.Model;

namespace MyAnimeWatchListTool.Repositories.Interfaces;

public interface ISuggestedAnimeRepository
{
    Task AddSuggestedAnime(SuggestedAnime suggestedAnime);
}