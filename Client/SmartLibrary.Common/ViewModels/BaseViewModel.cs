

namespace SmartLibrary.Common.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
        _title = GetType().Name;    
    }

    [ObservableProperty]
    string _title=default!;

    [ObservableProperty]
    bool _isBusy=false;

    [ObservableProperty]
    private string? _errorMessage;

    private TaskCompletionSource<bool> _waitForModalCompletion=default!;
    public Task<bool> ModalCompletionTask => (_waitForModalCompletion == default) ? Task.FromResult(false) : _waitForModalCompletion.Task;

    public virtual void OnNavigatedTo(IDictionary<string, object> data = default!)
    {
    }

    /// <summary>
    /// Call the base class method to make sure it works correctly
    /// </summary>
    /// <param name="query"></param>
    public virtual void OnNavigatedToModal(IDictionary<string, object> query)
    {
        _waitForModalCompletion = new TaskCompletionSource<bool>();
    }

    public virtual void SetModalComplete(bool result)
    {
        _waitForModalCompletion.SetResult(result);
    }
}
