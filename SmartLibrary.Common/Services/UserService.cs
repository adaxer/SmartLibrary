using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Common.Services;

public class UserService : IUserService
{
    public string UserName { get; set; }

    public bool IsLoggedIn => !string.IsNullOrEmpty(UserName);
}
