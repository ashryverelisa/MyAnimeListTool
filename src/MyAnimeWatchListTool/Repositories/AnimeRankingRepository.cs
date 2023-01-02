using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MyAnimeWatchListTool.Extensions;
using MyAnimeWatchListTool.Model;
using MyAnimeWatchListTool.Repositories.Interfaces;

namespace MyAnimeWatchListTool.Repositories;

public class AnimeRankingRepository : IAnimeRankingRepository
{
    private readonly ILogger<AnimeRankingRepository> _logger;
    private readonly IMongoCollection<AnimeRanking> _collection;
    private const string CollectionName = "AnimeRanking";

    public AnimeRankingRepository(IMongoClient mongoClient, ILogger<AnimeRankingRepository> logger)
    {
        _logger = logger;
        _collection = mongoClient.GetDefaultDatabase().GetCollection<AnimeRanking>(CollectionName);
    }

    public async Task AddAnime(AnimeRanking animeRanking)
    {
        var exist = await _collection.CountDocumentsAsync(x => x.Id == animeRanking.Id);

        if (exist > 0)
        {
            _logger.LogWarning("Anime {Name} with {Id} already exist.", animeRanking.Title, animeRanking.Id);
            return;
        }

        await _collection.InsertOneAsync(animeRanking);
    }
}