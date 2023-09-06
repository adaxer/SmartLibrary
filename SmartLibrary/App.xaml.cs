using SmartLibrary.Common.Interfaces;

namespace SmartLibrary;

public partial class App : Application
{
    private CheckItemService itemChecker;
    private readonly List<IRequireInitializeAsync> asyncInits;
    private readonly List<IRequireInitialize> syncInits;

    public App(IEnumerable<IRequireInitializeAsync> asyncs, IEnumerable<IRequireInitialize> syncs)
	{
        Strings.Culture = new CultureInfo("en-US");

        asyncInits = asyncs.ToList();
        syncInits = syncs.ToList();

        InitializeComponent();

		MainPage = new AppShell();
	}

    protected override void OnStart()
    {
        base.OnStart();
        syncInits.ForEach(s => s.Initialize());
        asyncInits.ForEach(a => a.InitializeAsync());
		itemChecker = new CheckItemService();
		itemChecker.Start();
    }
}
