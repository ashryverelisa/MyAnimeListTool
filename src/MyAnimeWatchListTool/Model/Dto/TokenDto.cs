namespace MyAnimeWatchListTool.Model.Dto;

public record TokenDto(string TokenType, int ExpiresIn, string AccessToken, string RefreshToken);