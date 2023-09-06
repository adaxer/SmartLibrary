namespace SmartLibrary;

public partial class App : Application
{
    private CheckItemService itemChecker;

    public App()
	{
        Strings.Culture = new CultureInfo("en-US");

        InitializeComponent();

		MainPage = new AppShell();
	}

    protected override void OnStart()
    {
        base.OnStart();
		itemChecker = new CheckItemService();
		itemChecker.Start();
    }
}
