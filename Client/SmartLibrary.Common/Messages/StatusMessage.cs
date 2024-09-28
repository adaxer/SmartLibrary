using CommunityToolkit.Mvvm.Messaging.Messages;

namespace SmartLibrary.Common.Messages;
public class StatusMessage : ValueChangedMessage<string>
{
    public StatusMessage(string value) : base(value)
    {
    }
}
