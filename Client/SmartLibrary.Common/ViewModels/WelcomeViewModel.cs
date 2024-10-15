
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Models;

namespace SmartLibrary.Common.ViewModels;

// Todo: Logout, Refresh token

public partial class WelcomeViewModel : BaseViewModel, IRecipient<UserInfoChangedMessage>
{
    public WelcomeViewModel(IPubSubService pubSubService)
    {
        pubSubService.Subscribe<UserInfoChangedMessage>(this);
        Title = Strings.Welcome;
    }

    public void Receive(UserInfoChangedMessage message)
    {
        var isLoggedIn = message.Value != UserInfo.None;
        Title = isLoggedIn
            ? $"{Strings.Welcome} {message.Value.UserName}"
            : $"{Strings.Welcome}";
    }
}
