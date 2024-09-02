namespace SmartLibrary.Common.Models;
public record class TokenResponse
{
    public string? TokenType { get; init; }
    public string? AccessToken { get; init; }
    public long ExpiresIn { get; init; }
    public string? RefreshToken { get; init; }
}