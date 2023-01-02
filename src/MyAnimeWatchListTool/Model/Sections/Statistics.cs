namespace MyAnimeWatchListTool.Model.Sections;

public class Statistics
{
    public Status Status { get; set; } = new();
    public int NumListUsers { get; set; }
}