namespace SmartLibrary.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void ChangeResource(object sender, EventArgs e)
    {
		Style style = new Style(typeof(Label));
		style.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = "Courier New" });

		theParent.Resources["ExtraText"] = style;
    }
}
