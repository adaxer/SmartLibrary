using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Common.Messages;
using SmartLibrary.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartLibrary.Wpf;
public partial class ShellViewModel : BaseViewModel
{
    public ShellViewModel(IServiceProvider serviceProvider, IMessenger messenger)
    {
        Modules = new ObservableCollection<IModuleViewModel>
        {
            serviceProvider.GetService<ModuleViewModel<MainViewModel>>(),
            serviceProvider.GetService<ModuleViewModel<SearchViewModel>>(),
            serviceProvider.GetService<ModuleViewModel<DetailsViewModel>>(),
            serviceProvider.GetService<ModuleViewModel<NewsViewModel>>(),
        };
        Modules.Single(m => m.TargetType.Equals(typeof(DetailsViewModel))).IsTopLevel = false;
        messenger.Register<NavigationMessage>(this, OnMessage);
    }

    private void OnMessage(object recipient, NavigationMessage message)
    {
        var type = message.Target.GetType();
        foreach (var module in Modules)
        {
            if (type == typeof(DetailsViewModel))
            {
                module.IsTopLevel = true;
            }
            else
            {
                module.IsTopLevel = module.TargetType!=typeof(DetailsViewModel);
            }
        }
    }

    public override void OnNavigatedTo(IDictionary<string, object> data = null)
    {
        base.OnNavigatedTo(data);
        Modules.First().NavigateCommand.Execute(null);
    }

    [ObservableProperty]
    ICollection<IModuleViewModel> _modules;

    [ObservableProperty]
    BaseViewModel _currentContent;
}
