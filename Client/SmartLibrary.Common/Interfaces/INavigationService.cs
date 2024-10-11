using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Common.Interfaces;
public interface INavigationService
{
    Task NavigateAsync<T>(params (string key, object value)[] data) where T : BaseViewModel;

    Task<(bool result, T dialog)> ShowDialogAsync<T>(params (string key, object value)[] data) where T: BaseViewModel;

    Func<string, string> GetViewName { get; set; }

    bool CanGoBack() => false;

    Task GoBack() => Task.CompletedTask;
}
