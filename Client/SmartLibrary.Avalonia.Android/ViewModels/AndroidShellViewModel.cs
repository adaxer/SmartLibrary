using CommunityToolkit.Mvvm.ComponentModel;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Core.Localization;

namespace SmartLibrary.Avalonia.Android.ViewModels;
public partial class AndroidShellViewModel : ShellViewModel
{
    public AndroidShellViewModel(WelcomeViewModel welcome, INavigationService navigationService, ILocalizationService localizationService, IPubSubService pubSub) : base(welcome, navigationService, localizationService, pubSub)
    {
    }

    [ObservableProperty]
    BaseViewModel? _currentDialog=default;
}
