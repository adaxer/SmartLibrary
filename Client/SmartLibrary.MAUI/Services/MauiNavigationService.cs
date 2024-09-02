using System.Reflection;

namespace SmartLibrary.MAUI.Services;
public class MauiNavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    public MauiNavigationService(IServiceProvider serviceProvider)
    {
        GetViewName = GetViewNameDefault;
        _serviceProvider = serviceProvider;
    }

    public Func<string, string> GetViewName
    {
        get;
        set;
    }

    private string GetViewNameDefault(string targetName)
    {
        return targetName.Replace("ViewModel", "Page");
    }

    public async Task NavigateAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var pageName = GetViewName(typeof(T).Name);
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        await Shell.Current.GoToAsync(pageName, true, parameters);
    }

    public async Task<(bool result, T dialog)> ShowDialogAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var pageName = GetViewName(typeof(T).Name);
        var pageType = GetType().Assembly.GetTypes().SingleOrDefault(t => t.Name == pageName);
        var page = _serviceProvider.GetService(pageType!) as BasePage;
        if (page == null) // Not allowed
        {
            throw new InvalidOperationException("Navigation only possible to pages deriving from BasePage");
        }
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        page.SuppressApplyQueryAttributes = true;
        await Shell.Current.Navigation.PushModalAsync(page, true);
        page.SuppressApplyQueryAttributes = false;
        page.ApplyQueryAttributesModal(parameters);
        var result = await page.WaitForPopModalAsync();
        return (result, page.ViewModel as T)!;
    }
}
