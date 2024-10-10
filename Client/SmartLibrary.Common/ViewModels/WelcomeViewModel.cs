
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Common.ViewModels;

// Todo: Logout, Refresh token

public partial class WelcomeViewModel : BaseViewModel
{
    private readonly IUserClient _userService;
    private readonly INavigationService _navigationService;

    public WelcomeViewModel(IUserClient userService, INavigationService navigationService)
    {
        _userService = userService;
        _navigationService = navigationService;
        SetWelcomeAsync();
    }

    [ObservableProperty]
    private bool _isLoggedIn;

    [RelayCommand]
    private async Task LoginAsync()
    {
        var dialog = await _navigationService.ShowDialogAsync<LoginViewModel>(("Source", this));
        SetWelcomeAsync();
    }

    [RelayCommand]
    private void Logout()
    {
        _userService.Logout();
        SetWelcomeAsync();
    }

    private async void SetWelcomeAsync()
    {
        IsLoggedIn = await _userService.GetIsLoggedInAsync();
        Title = IsLoggedIn
            ? $"{Strings.Welcome} {_userService.UserName}"
            : $"{Strings.Welcome}";
    }
}
