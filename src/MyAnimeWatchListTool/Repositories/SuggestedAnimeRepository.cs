using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MyAnimeWatchListTool.Extensions;
using MyAnimeWatchListTool.Model;
using MyAnimeWatchListTool.Repositories.Interfaces;

namespace MyAnimeWatchListTool.Repositories;

public class SuggestedAnimeRepository : ISuggestedAnimeRepository
{
    private readonly ILogger<SuggestedAnimeRepository> _logger;
    private readonly IMongoCollection<SuggestedAnime> _collection;
    private const string CollectionName = "SuggestedAnime";

    public SuggestedAnimeRepository(IMongoClient mongoClient, ILogger<SuggestedAnimeRepository> logger)
    {
        _logger = logger;
        _collection = mongoClient.GetDefaultDatabase().GetCollection<SuggestedAnime>(CollectionName);
    }


    public async Task AddSuggestedAnime(SuggestedAnime suggestedAnime)
    {
        var exist = await _collection.CountDocumentsAsync(x => x.Id == suggestedAnime.Id);

        if (exist > 0)
        {
            _logger.LogWarning("Anime {Name} with {Id} already exist.", suggestedAnime.Title, suggestedAnime.Id);
            return;
        }

        await _collection.InsertOneAsync(suggestedAnime);
    }
}