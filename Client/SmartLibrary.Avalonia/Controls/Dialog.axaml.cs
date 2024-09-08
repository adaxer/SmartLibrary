using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia;

public partial class Dialog : Window
{
    public Dialog()
    {
        InitializeComponent();
        DataContextChanged += (_, _) =>
        {
            if (DataContext is BaseViewModel viewModel) CheckForCloseAsync(viewModel);
        };
    }
    private async void CheckForCloseAsync(BaseViewModel viewModel)
    {
        do
        {
            await Task.Delay(300);
        } while (!viewModel.ModalCompletionTask.IsCompleted);

        Close();
    }

    protected override void OnClosed(EventArgs e)
    {
        if (DataContext is BaseViewModel viewModel)
        {
            if (!viewModel.ModalCompletionTask.IsCompleted)
            {
                viewModel.SetModalComplete(false);
            }
        }
        base.OnClosed(e);
    }
}
