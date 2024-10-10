using Avalonia.Animation.Easings;
using Avalonia.Animation;
using Avalonia;
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;
using System.Threading.Tasks;
using SmartLibrary.Avalonia.Interfaces;

namespace SmartLibrary.Avalonia.Android.Views;
public partial class ShellView : UserControl, IShellView
{
    public ShellView(IShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    protected override async void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        var marginAnimation = new Animation
        {
            Duration = TimeSpan.FromMilliseconds(1000),
            Easing = new CubicEaseOut(),
            Children =
            {
                new KeyFrame
                {
                    Setters = {
                        new Setter(MarginProperty, new Thickness(0, 0, -300, 0)),
                        new Setter(OpacityProperty, 0.0)
                    },
                    Cue = new Cue(0d)
                },
                new KeyFrame
                {
                    Setters = {
                        new Setter(MarginProperty, new Thickness(0, 0, 0, 0)),
                        new Setter(OpacityProperty, 1.0)
                    },
                    Cue = new Cue(1d)
                }
            }
        };

        await Task.Delay(4000);
        await marginAnimation.RunAsync(_internetCheck);

        _internetCheck.Margin = new();
    }
}
