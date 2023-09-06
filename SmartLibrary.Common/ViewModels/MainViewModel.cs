using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Common.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public MainViewModel(ILocationService locationService)
    {
        this.locationService = locationService;
        SetWelcome();
    }

    private async void SetWelcome()
    {
        try
        {
            Location loc = await locationService.GetLocationAsync();
            Title = $"{Strings.Welcome} in Lat: {loc.Latitude}, Lng: {loc.Longitude}";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
            Title = $"{Strings.Welcome} (somewhere)";
        }
    }

    string userName = string.Empty;
    private readonly ILocationService locationService;

    public string UserName
    {
        get => userName;
        set
        {
            SetProperty(ref userName, value);
            SetUserCommand.NotifyCanExecuteChanged();
        }
    }

    private bool CanSetUser() => !string.IsNullOrEmpty(UserName);

    [RelayCommand(CanExecute = nameof(CanSetUser))]
    private Task SetUser()
    {
        // Vielleicht mit UserService, der in App bekannt ist
        Title = $"{Strings.Welcome}, {UserName}";
        return Task.CompletedTask;
    }
}
