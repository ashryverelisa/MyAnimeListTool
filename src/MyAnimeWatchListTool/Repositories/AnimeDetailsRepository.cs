using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MyAnimeWatchListTool.Extensions;
using MyAnimeWatchListTool.Model;
using MyAnimeWatchListTool.Repositories.Interfaces;

namespace MyAnimeWatchListTool.Repositories;

public class AnimeDetailsRepository : IAnimeDetailsRepository
{
    private readonly ILogger<AnimeDetailsRepository> _logger;
    private readonly IMongoCollection<AnimeDetails> _collection;
    private const string CollectionName = "AnimeDetails";

    public AnimeDetailsRepository(IMongoClient mongoClient, ILogger<AnimeDetailsRepository> logger)
    {
        _logger = logger;
        _collection = mongoClient.GetDefaultDatabase().GetCollection<AnimeDetails>(CollectionName);
    }

    public async Task AddAnimeDetails(AnimeDetails animeDetails)
    {
        var exist = await _collection.CountDocumentsAsync(x => x.Id == animeDetails.Id);

        if (exist > 0)
        {
            _logger.LogWarning("Anime {Name} with {Id} already exist.", animeDetails.Title, animeDetails.Id);
            return;
        }

        await _collection.InsertOneAsync(animeDetails);
    }
}