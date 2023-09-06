using CommunityToolkit.Mvvm.Messaging.Messages;

namespace SmartLibrary.Common.Messages;

public class SharedBookMessage : ValueChangedMessage<SavedBook>
{
    public SharedBookMessage(SavedBook value) : base(value)
    {
    }
}