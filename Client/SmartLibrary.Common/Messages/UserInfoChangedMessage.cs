using CommunityToolkit.Mvvm.Messaging.Messages;
using SmartLibrary.Common.Models;

namespace SmartLibrary.Common;
public class UserInfoChangedMessage : ValueChangedMessage<UserInfo>
{
    public UserInfoChangedMessage(UserInfo value) : base(value)
    {
    }
}
