namespace MyAnimeWatchListTool.Model.Sections;

public class Node
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Picture MainPicture { get; set; } = new();
}