namespace SmartLibrary.Common.Messages;

public record class NavigationMessage(BaseViewModel Target, bool IsModal);

