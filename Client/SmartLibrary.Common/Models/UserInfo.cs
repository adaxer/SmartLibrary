namespace SmartLibrary.Common.Models;

public class UserInfo
{
    public static UserInfo None = _none??=_none=new();

    private static UserInfo _none;

    public string? Email { get; set; }
    public string? UserName { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
    public string? AccessToken { get; set; }

    // public string? RefreshToken { get; init; }
}
