
namespace SmartLibrary.MAUI.Views;

public partial class SearchPage : BasePage
{
	public SearchPage(SearchViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
        SetSearchTextAsync();
	}

    private async void SetSearchTextAsync()
    {
        while (!(BindingContext is SearchViewModel))
            await Task.Delay(10);
        (BindingContext as  SearchViewModel)!.SearchText="MAUI";
    }
}
