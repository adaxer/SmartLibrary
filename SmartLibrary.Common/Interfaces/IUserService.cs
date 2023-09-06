namespace SmartLibrary.Common.Interfaces;

public interface IUserService
{
    string UserName { get; set; }
    bool IsLoggedIn { get; }
}
