using Avalonia;
using Avalonia.Controls;

namespace SmartLibrary.Avalonia.Controls;
public class LabelAndUnit : ContentControl
{
    public string Label
    {
        get => GetValue(LabelProperty); 
        set => SetValue(LabelProperty, value); 
    }

    public static readonly StyledProperty<string> LabelProperty =
        AvaloniaProperty.Register<LabelAndUnit, string>(nameof(Label), string.Empty);

    public string Unit
    {
        get => GetValue(UnitProperty); 
        set => SetValue(UnitProperty, value); 
    }

    public static readonly StyledProperty<string> UnitProperty =
        AvaloniaProperty.Register<LabelAndUnit,string>(nameof(Unit), string.Empty);

    public double LabelMargin
    {
        get => GetValue(LabelMarginProperty);
        set => SetValue(LabelMarginProperty, value);
    }

    public static readonly StyledProperty<double> LabelMarginProperty =
        AvaloniaProperty.Register<LabelAndUnit,double>("LabelMargin", 10.0);

    public double UnitMargin
    {
        get => GetValue(UnitMarginProperty);
        set => SetValue(UnitMarginProperty, value);
    }

    public static readonly StyledProperty<double> UnitMarginProperty =
        AvaloniaProperty.Register<LabelAndUnit,double>("UnitMargin", 6.0);
}

