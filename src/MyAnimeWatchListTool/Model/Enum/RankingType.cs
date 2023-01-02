namespace MyAnimeWatchListTool.Model.Enum;

public enum RankingType
{
    All,
    Airing,
    Upcoming,
    Tv,
    Ova,
    Movie,
    Special,
    ByPopularity,
    Favorite
}

public static class RankingTypeExtensions
{
    public static string ToLower(this RankingType rankingType)
    {
        return rankingType.ToString().ToLower();
    }
}