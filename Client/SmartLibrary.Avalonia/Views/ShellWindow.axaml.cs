using Avalonia.Animation.Easings;
using Avalonia.Animation;
using Avalonia;
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Rendering.Composition.Animations;
using Avalonia.Styling;
using System.Threading.Tasks;

namespace SmartLibrary.Avalonia.Views;
public partial class ShellWindow : Window
{
    public ShellWindow()
    {
        InitializeComponent();
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
                        new Setter(StackPanel.MarginProperty, new Thickness(0, 0, -300, 0)), 
                        new Setter(StackPanel.OpacityProperty, 0.0) 
                    },
                    Cue = new Cue(0d)
                },
                new KeyFrame
                {
                    Setters = { 
                        new Setter(StackPanel.MarginProperty, new Thickness(0, 0, 0, 0)),
                        new Setter(StackPanel.OpacityProperty, 1.0)
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
