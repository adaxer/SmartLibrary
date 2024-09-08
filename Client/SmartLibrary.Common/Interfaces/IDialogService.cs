namespace SmartLibrary.Common.Interfaces;

public interface IDialogService
{
    Task<(bool result, T content)> ShowDialogAsync<T>(T content, IDictionary<string, object> data) where T : BaseViewModel;
}
