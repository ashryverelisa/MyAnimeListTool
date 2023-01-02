using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MyAnimeWatchListTool.Extensions;
using MyAnimeWatchListTool.Model;
using MyAnimeWatchListTool.Repositories.Interfaces;

namespace MyAnimeWatchListTool.Repositories;

public class SeasonalAnimeRepository : ISeasonalAnimeRepository
{
    private readonly ILogger<SeasonalAnimeRepository> _logger;
    private readonly IMongoCollection<SeasonalAnime> _collection;
    private const string CollectionName = "SeasonalAnime";

    public SeasonalAnimeRepository(IMongoClient mongoClient, ILogger<SeasonalAnimeRepository> logger)
    {
        _logger = logger;
        _collection = mongoClient.GetDefaultDatabase().GetCollection<SeasonalAnime>(CollectionName);
    }

    public async Task AddSeasonalAnime(SeasonalAnime seasonalAnime)
    {
        var exist = await _collection.CountDocumentsAsync(x => x.Id == seasonalAnime.Id);

        if (exist > 0)
        {
            _logger.LogWarning("Anime {Name} with {Id} already exist.", seasonalAnime.Title, seasonalAnime.Id);
            return;
        }

        await _collection.InsertOneAsync(seasonalAnime);
    }
}