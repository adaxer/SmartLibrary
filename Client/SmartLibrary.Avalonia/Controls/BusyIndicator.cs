using Avalonia;
using Avalonia.Controls.Primitives;

namespace SmartLibrary.Avalonia.Controls;
public class BusyIndicator : TemplatedControl
{
    public bool IsBusy
    {
        get => GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public static readonly StyledProperty<bool> IsBusyProperty =
        AvaloniaProperty.Register<BusyIndicator, bool>(nameof(IsBusy), false);
}
