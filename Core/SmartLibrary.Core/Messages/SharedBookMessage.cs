using CommunityToolkit.Mvvm.Messaging.Messages;

namespace SmartLibrary.Core.Messages;

public class SharedBookMessage : ValueChangedMessage<SavedBook>
{
    public SharedBookMessage(SavedBook value) : base(value)
    {
    }
}