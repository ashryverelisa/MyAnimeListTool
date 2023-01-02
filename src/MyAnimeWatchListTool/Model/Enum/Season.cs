namespace MyAnimeWatchListTool.Model.Enum;

public enum Season
{
    Winter,
    Spring,
    Summer,
    Fall
}

public static class SeasonExtensions
{
    public static string ToLower(this Season season)
    {
        return season.ToString().ToLower();
    }
}