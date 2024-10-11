using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using SmartLibrary.Avalonia.iOS.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.iOS.Platform;

public class IOSDialogService : IDialogService
{
    public async Task<(bool result, T content)> ShowDialogAsync<T>(T content, IDictionary<string, object> data) where T : BaseViewModel
    {
        var shell = (global::Avalonia.Application.Current!.ApplicationLifetime as ISingleViewApplicationLifetime)!.MainView!.DataContext as IOSShellViewModel;
        shell!.CurrentDialog = content;
        content.OnNavigatedToModal(data);
        var result = await content.ModalCompletionTask;
        shell.CurrentDialog = default;
        return (result, content);
    }
}
