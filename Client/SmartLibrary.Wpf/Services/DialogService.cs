using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Wpf.Controls;
using SmartLibrary.Wpf.Interfaces;

namespace SmartLibrary.Wpf.Services;

public class DialogService : IDialogService
{
    public async Task<(bool result, T content)> ShowDialogAsync<T>(T content, IDictionary<string, object> data) where T : BaseViewModel
    {
        var dlg = new Dialog { WindowStartupLocation=WindowStartupLocation.CenterOwner, Owner=Application.Current.MainWindow, DataContext=content };
        dlg.SetBinding(Dialog.ContentProperty, "Content");
        content.OnNavigatedToModal(data);
        dlg.ShowDialog();
        var result = await content.ModalCompletionTask;
        return (result, content);
    }
}
