namespace SmartLibrary.Common.Interfaces;
public interface INavigationService
{
    Dictionary<string, object> NavigationParameters(string targetViewModel);

    Task GoToAsync(string target, Dictionary<string, object> parameters);
}
