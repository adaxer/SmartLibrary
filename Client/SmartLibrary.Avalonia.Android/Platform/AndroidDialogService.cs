using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using SmartLibrary.Avalonia.Android.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.Android.Platform;

public class AndroidDialogService : IDialogService
{
    public async Task<(bool result, T content)> ShowDialogAsync<T>(T content, IDictionary<string, object> data) where T : BaseViewModel
    {
        var shell = (Application.Current!.ApplicationLifetime as ISingleViewApplicationLifetime)!.MainView!.DataContext as AndroidShellViewModel;
        shell!.CurrentDialog = content;
        content.OnNavigatedToModal(data);
        var result = await content.ModalCompletionTask;
        shell.CurrentDialog = default;
        return (result, content);
    }
}
