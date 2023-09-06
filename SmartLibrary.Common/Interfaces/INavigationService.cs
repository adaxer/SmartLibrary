namespace SmartLibrary.Common.Interfaces;
public interface INavigationService
{
    Task GoToAsync(string target, Dictionary<string, object> parameters);
}
