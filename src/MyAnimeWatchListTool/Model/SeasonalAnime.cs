using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyAnimeWatchListTool.Model.Sections;

namespace MyAnimeWatchListTool.Model;

public class SeasonalAnime
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Picture MainPicture { get; set; } = new();
    public int Year { get; set; }
    public string Season { get; set; } = string.Empty;
}