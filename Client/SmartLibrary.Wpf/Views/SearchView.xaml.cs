using System.Windows.Controls;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Core.Models;

namespace SmartLibrary.Wpf.Views;
/// <summary>
/// Interaction logic for SearchView.xaml
/// </summary>
public partial class SearchView : UserControl
{
    public SearchView()
    {
        InitializeComponent();
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        (DataContext as SearchViewModel)?.GoToDetailsCommand?.Execute((sender as ListView)?.SelectedItem as Book);
    }
}
