using CommunityToolkit.Mvvm.ComponentModel;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Core.Localization;

namespace SmartLibrary.Avalonia.iOS.ViewModels;
public partial class IOSShellViewModel : ShellViewModel
{
    public IOSShellViewModel(WelcomeViewModel welcome, INavigationService navigationService, ILocalizationService localizationService, IPubSubService pubSub) : base(welcome, navigationService, localizationService, pubSub)
    {
    }

    [ObservableProperty]
    BaseViewModel? _currentDialog = default;
}
