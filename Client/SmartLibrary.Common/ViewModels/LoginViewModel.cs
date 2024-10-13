

using SmartLibrary.Common.Models;

namespace SmartLibrary.Common.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IUserClient _userService;
    private readonly ILocalizationService _localizationService;

    public LoginViewModel(IUserClient userService, ILocalizationService localizationService)
    {
        _userService = userService;
        _localizationService = localizationService;
    }

    [ObservableProperty]
    private string _userName = "bob";

    [ObservableProperty]
    private string _email = default!;

    [ObservableProperty]
    private string _password = "123User!";

    [ObservableProperty]
    private bool _isLoggedIn = false;

    [ObservableProperty]
    private string _loggedInName=string.Empty;

    [RelayCommand]
    private void Logout()
    {
        _userService.Logout();
        SetLoginInfo();
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        ErrorMessage = string.Empty;
        if (!(await _userService.GetIsLoggedInAsync()))
        {
            var loginSuccessful = await _userService.LoginAsync(UserName, Email, Password);
            if (loginSuccessful)
            {
                SetModalComplete(true);
            }
            else
            {
                ErrorMessage = string.Format(Strings.LoginFailed, UserName + (string.IsNullOrEmpty(Email) ? string.Empty : $" {Email}"));
            }
        }
        SetLoginInfo();
    }

    public override void OnNavigatedToModal(IDictionary<string, object> data)
    {
        base.OnNavigatedToModal(data);
        SetLoginInfo();
    }

    private void SetLoginInfo()
    {
        IsLoggedIn = (_userService.UserInfo != UserInfo.None);
        LoggedInName = string.Format(_localizationService.Get("LoggedInName"), _userService.UserInfo!.UserName);
    }

    [RelayCommand]
    private void Cancel()
    {
        SetModalComplete(false);
    }
}
