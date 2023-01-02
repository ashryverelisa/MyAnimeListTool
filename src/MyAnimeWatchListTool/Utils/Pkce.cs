using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyAnimeWatchListTool.Utils;

public class Pkce
{
    public Pkce(uint size = 128)
    {
        var codeVerifier = GenerateCodeVerifier(size);
        GenerateCodeChallenge(codeVerifier);
    }

    public static string GenerateCodeVerifier(uint size = 128)
    {
        if (size is < 43 or > 128)
            size = 128;

        const string unreservedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
        var random = new Random();
        var highEntropyCryptograph = new char[size];

        for (var i = 0; i < highEntropyCryptograph.Length; i++)
        {
            highEntropyCryptograph[i] = unreservedCharacters[random.Next(unreservedCharacters.Length)];
        }

        return new string(highEntropyCryptograph);
    }

    public static string GenerateCodeChallenge(string codeVerifier)
    {
        var challengeBytes = SHA256.HashData(Encoding.UTF8.GetBytes(codeVerifier));
        return Base64UrlEncoder.Encode(challengeBytes);
    }
}