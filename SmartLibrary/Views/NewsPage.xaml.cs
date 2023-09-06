namespace SmartLibrary.Views;

public partial class NewsPage : ContentPage
{
	public NewsPage(NewsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
