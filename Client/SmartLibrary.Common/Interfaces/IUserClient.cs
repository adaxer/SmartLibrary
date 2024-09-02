using SmartLibrary.Common.Models;

namespace SmartLibrary.Common.Interfaces;

public interface IUserClient
{
    string? UserName { get; }

    string? Email { get; }

    UserInfo? UserInfo { get; }

    Task<bool> LoginAsync(string userName, string email, string password);

    Task<bool> GetIsLoggedInAsync();

    void Logout();
}
