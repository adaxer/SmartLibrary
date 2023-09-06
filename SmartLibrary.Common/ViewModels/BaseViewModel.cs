namespace SmartLibrary.Common.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    string title;

    [ObservableProperty]
    bool isBusy;
}
