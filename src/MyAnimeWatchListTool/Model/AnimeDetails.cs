using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyAnimeWatchListTool.Model.Sections;

namespace MyAnimeWatchListTool.Model;

public class AnimeDetails
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Picture Pictures { get; set; } = new();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float Mean { get; set; }
    public int Rank { get; set; }
    public int Popularity { get; set; }
    public int NumListUsers { get; set; }
    public int NumScoringUsers { get; set; }
    public string MediaType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<Genre> Genres { get; set; } = new();
    public int NumEpisodes { get; set; }
    public int AverageEpisodeDuration { get; set; }
    public string Rating { get; set; } = string.Empty;
    public List<Studio> Studios { get; set; } = new();
    public Statistics Statistics { get; set; } = new();
}