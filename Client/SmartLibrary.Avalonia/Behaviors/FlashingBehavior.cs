using Avalonia;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;
using System.Threading.Tasks;

namespace SmartLibrary.Avalonia.Behaviors;

public class FlashingBehavior : Behavior<TextBlock>
{
    public static readonly StyledProperty<int> FlashDurationProperty =
         AvaloniaProperty.Register<FlashingBehavior, int>(nameof(FlashDuration), 300);

    public int FlashDuration
    {
        get => GetValue(FlashDurationProperty);
        set => SetValue(FlashDurationProperty, value);
    }
    public static readonly StyledProperty<int> FlashCountProperty =
         AvaloniaProperty.Register<FlashingBehavior, int>(nameof(FlashCount), 1);

    public int FlashCount
    {
        get => GetValue(FlashCountProperty);
        set => SetValue(FlashCountProperty, value);
    }
    protected override void OnAttached()
    {
        base.OnAttached();
        if (AssociatedObject != null)
        {
            AssociatedObject.PropertyChanged += PropertyChangedAsync;
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject != null)
        {
            AssociatedObject.PropertyChanged -= PropertyChangedAsync;
        }
    }

    private async void PropertyChangedAsync(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == TextBlock.TextProperty)
        {
            for (int i = 0; i < FlashCount; i++)
            {
                AssociatedObject!.Opacity = 0.0;
                await Task.Delay(FlashDuration);
                AssociatedObject.Opacity = 1.0;
            }
        }
    }
}
