using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Wpf.Controls;
public class Dialog : Window
{
    static Dialog()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Dialog), new FrameworkPropertyMetadata(typeof(Dialog)));
    }

    public Dialog()
    {
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

    public bool ShowCommands
    {
        get { return (bool)GetValue(ShowCommandsProperty); }
        set { SetValue(ShowCommandsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ShowCommands.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ShowCommandsProperty =
        DependencyProperty.Register("ShowCommands", typeof(bool), typeof(Dialog), new PropertyMetadata(false));



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
