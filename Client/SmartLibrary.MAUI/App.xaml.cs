
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.MAUI;

public partial class App : Application
{
    private readonly List<IRequireInitializeAsync> _asyncInits;
    private readonly List<IRequireInitialize> _syncInits;

    public App(IEnumerable<IRequireInitializeAsync> asyncs, IEnumerable<IRequireInitialize> syncs)
	{
        Strings.Culture = new CultureInfo("en-US");

        _asyncInits = [.. asyncs];
        _syncInits = [.. syncs];

        InitializeComponent();

#pragma warning disable CS0618 // Type or member is obsolete
        MainPage = new AppShell();
#pragma warning restore CS0618 // Type or member is obsolete
    }

    protected override void OnStart()
    {
        base.OnStart();
        _syncInits.ForEach(s => s.Initialize());
        _asyncInits.ForEach(a => a.InitializeAsync());
    }
}
