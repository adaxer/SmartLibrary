using NSubstitute;
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Common.ViewModels;

public static class DesignData
{
    public static LoginViewModel DesignLoginViewModel => new LoginViewModel(Substitute.For<IUserClient>())
    {
        UserName = "DesignUser",
        Email = "User@Design.com"
    };
}
