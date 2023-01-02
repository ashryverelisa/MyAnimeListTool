using MongoDB.Driver;

namespace MyAnimeWatchListTool.Extensions;

public static class MongoClientExtensions
{
    public static IMongoDatabase GetDefaultDatabase(this IMongoClient mongoClient)
    {
        return mongoClient.GetDatabase("WatchList");
    }
}