namespace SmartLibrary.Views;

public partial class SearchDetailPage : ContentPage
{
	public SearchDetailPage(SearchDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
