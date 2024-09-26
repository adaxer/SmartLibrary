using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace SmartLibrary.Avalonia.Views;

public partial class SearchView : UserControl
{
    ScrollViewer _scrollViewer = default!;
    public SearchView()
    {
        InitializeComponent();
        Loaded += (_,_) =>
        {
            _scrollViewer = _listBox.FindDescendantOfType<ScrollViewer>()!;
            _scrollViewer!.ScrollChanged+=SearchView_ScrollChanged;
        };
    }

    private void SearchView_ScrollChanged(object? sender, ScrollChangedEventArgs e)
    {
        Trace.WriteLine($"Scroll-Position: {_scrollViewer.Offset.Y} / {_scrollViewer.ScrollBarMaximum.Y} = {(100*_scrollViewer.Offset.Y)/_scrollViewer.ScrollBarMaximum.Y} %");
    }
}
