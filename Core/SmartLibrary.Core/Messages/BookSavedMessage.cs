using CommunityToolkit.Mvvm.Messaging.Messages;

namespace SmartLibrary.Core.Messages;

public class BookSavedMessage : ValueChangedMessage<SavedBook>
{
    public BookSavedMessage(SavedBook value) : base(value)
    {
    }
}