using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.Services;

public class AvaloniaDialogService : IDialogService
{
    public async Task<(bool result, T content)> ShowDialogAsync<T>(T content, IDictionary<string, object> data) where T : BaseViewModel
    {
        var dialog = new Dialog() { DataContext = content, SizeToContent = SizeToContent.WidthAndHeight, WindowStartupLocation = WindowStartupLocation.CenterOwner };
        content.OnNavigatedToModal(data);
        await dialog.ShowDialog((Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!.MainWindow!);
        var result = await content.ModalCompletionTask;
        return (result, content);
    }
}
