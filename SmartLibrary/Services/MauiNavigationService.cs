using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Services;
public class MauiNavigationService : INavigationService
{
    Dictionary<string, Dictionary<string, object>> currentParameters = new();

    public Task GoToAsync(string targetViewModel, Dictionary<string, object> parameters)
    {
        currentParameters[targetViewModel] = parameters;
        return Shell.Current.GoToAsync(GetViewName(targetViewModel), true, parameters);
    }

    public Dictionary<string, object> NavigationParameters(string targetViewModel)
    {
        if (currentParameters.TryGetValue(targetViewModel, out var savedParameters))
        {
            currentParameters.Clear();
            return savedParameters;
        }
        return null;
    }

    private ShellNavigationState GetViewName(string targetViewModel)
    {
        return targetViewModel.Replace("ViewModel", "Page");
    }
}
