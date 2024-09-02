namespace SmartLibrary.Common.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IUserClient _userService;

    public LoginViewModel(IUserClient userService)
    {
        _userService = userService;
    }

    [ObservableProperty]
    private string _userName = "bob";

    [ObservableProperty]
    private string _email = default!;

    [ObservableProperty]
    private string _password = "123User!";


    [RelayCommand]
    private async Task LoginAsync()
    {
        if (!(await _userService.GetIsLoggedInAsync()))
        {
            var loginSuccessful = await _userService.LoginAsync(UserName, Email, Password);
            if (loginSuccessful)
            {
                SetModalComplete(true);
            }
            else
            {
                ErrorMessage = string.Format(Strings.LoginFailed, UserName + (string.IsNullOrEmpty(Email)?string.Empty:$" {Email}"));
            }
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        SetModalComplete(false);
    }
}
