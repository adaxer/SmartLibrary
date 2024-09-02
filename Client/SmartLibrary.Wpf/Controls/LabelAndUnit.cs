using System.Windows;
using System.Windows.Controls;

namespace SmartLibrary.Wpf.Controls;
public class LabelAndUnit : ContentControl
{
    static LabelAndUnit()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelAndUnit), new FrameworkPropertyMetadata(typeof(LabelAndUnit)));
    }

    public string Label
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string), typeof(LabelAndUnit), new PropertyMetadata(null));



    public string Unit
    {
        get { return (string)GetValue(UnitProperty); }
        set { SetValue(UnitProperty, value); }
    }

    public static readonly DependencyProperty UnitProperty =
        DependencyProperty.Register("Unit", typeof(string), typeof(LabelAndUnit), new PropertyMetadata(null));



    public double LabelMargin
    {
        get { return (double)GetValue(LabelMarginProperty); }
        set { SetValue(LabelMarginProperty, value); }
    }

    public static readonly DependencyProperty LabelMarginProperty =
        DependencyProperty.Register("LabelMargin", typeof(double), typeof(LabelAndUnit), new PropertyMetadata(10.0));


    public double UnitMargin
    {
        get { return (double)GetValue(UnitMarginProperty); }
        set { SetValue(UnitMarginProperty, value); }
    }

    public static readonly DependencyProperty UnitMarginProperty =
        DependencyProperty.Register("UnitMargin", typeof(double), typeof(LabelAndUnit), new PropertyMetadata(6.0));




    public Style LabelStyle
    {
        get { return (Style)GetValue(LabelStyleProperty); }
        set { SetValue(LabelStyleProperty, value); }
    }

    public static readonly DependencyProperty LabelStyleProperty =
        DependencyProperty.Register("LabelStyle", typeof(Style), typeof(LabelAndUnit), new PropertyMetadata(null));



}
