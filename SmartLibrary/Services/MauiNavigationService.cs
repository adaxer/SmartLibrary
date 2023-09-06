using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Services;
public class MauiNavigationService : INavigationService
{
    public async Task GoToAsync(string target, Dictionary<string, object> parameters)
    {
        string targetPage = target.Replace("ViewModel", "Page");
        await Shell.Current.GoToAsync(targetPage, parameters);
    }
}
