using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows;

namespace SmartLibrary.Wpf.Behaviors;

public class WebBrowserBehavior : Behavior<WebBrowser>
{
    public static readonly DependencyProperty HtmlProperty =
        DependencyProperty.Register(nameof(Html), typeof(string), typeof(WebBrowserBehavior), new PropertyMetadata(OnHtmlChanged));

    public string Html
    {
        get { return (string)GetValue(HtmlProperty); }
        set { SetValue(HtmlProperty, value); }
    }

    private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is WebBrowserBehavior behavior && behavior.AssociatedObject != null)
        {
            string newHtml = e.NewValue as string;
            behavior.AssociatedObject.NavigateToString(newHtml ?? "<html><body>Cannot render the Html - check the string</body></html>");
        }
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        if (Html != null)
        {
            AssociatedObject.NavigateToString(Html);
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
    }
}
