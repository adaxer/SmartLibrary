
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Common.ViewModels;

// Todo: Logout, Refresh token

public partial class AboutViewModel : BaseViewModel
{
    public AboutViewModel()
    {
        Title = "Über SmartLibrary";
    }

    [RelayCommand]
    private void Close()
    {
        SetModalComplete(true);
    }
}
