using System.Windows;
using System.Windows.Controls;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Wpf.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class ShellWindow : Window
{
    public ShellWindow(ShellViewModel shell)
    {
        InitializeComponent();
        DataContext = shell;
    }

}
