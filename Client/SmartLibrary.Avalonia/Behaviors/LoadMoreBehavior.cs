using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactivity;
using System.Windows.Input;

namespace SmartLibrary.Avalonia.Behaviors;

public class LoadMoreBehavior : Behavior<ItemsControl>
{
    public static readonly StyledProperty<ICommand> LoadMoreCommandProperty =
         AvaloniaProperty.Register<LoadMoreBehavior, ICommand>(nameof(LoadMoreCommand));
    private ScrollViewer? _scrollViewer;

    public ICommand LoadMoreCommand
    {
        get => GetValue(LoadMoreCommandProperty);
        set => SetValue(LoadMoreCommandProperty, value);
    }

    protected override void OnAttached()
    {
        // Sobald dieses Behavior auf einem ItemsControl gesetzt wird,
        // wird OnAttached von Avalonia gerufen und der OnloadedEvent abgewartet

        base.OnAttached();

        if (AssociatedObject != null)
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }
    }

    private void AssociatedObject_Loaded(object? sender, RoutedEventArgs e)
    {
        //  ScrollChangedEvent (eines enthaltenen ScrollViewers) fangen 
        _scrollViewer = AssociatedObject.FindDescendantOfType<ScrollViewer>();
        if (_scrollViewer == null)
        {
            return; // Kein ScrollViewer - kein LoadMore 
        }

        _scrollViewer.ScrollChanged += OnScrollChanged;
        AssociatedObject!.Loaded -= AssociatedObject_Loaded;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (_scrollViewer != null)
        {
            _scrollViewer.ScrollChanged -= OnScrollChanged;
        }
    }

    private void OnScrollChanged(object? sender, ScrollChangedEventArgs e)
    {
        if(_scrollViewer==null)
        {
            return;
        }
        if (_scrollViewer.Offset.Y >= _scrollViewer.ScrollBarMaximum.Y && _scrollViewer.Offset.Y>0)
        {
            if (LoadMoreCommand?.CanExecute(null) == true)
            {
                LoadMoreCommand.Execute(null);
            }
        }
    }
}
