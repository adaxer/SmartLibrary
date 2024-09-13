using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;

namespace CustomDrawDemo;
public class Poly : Control
{
    public static readonly StyledProperty<int> PointsProperty =
        AvaloniaProperty.Register<Poly, int>(nameof(Points), 3, validate: value => value >= 3);

    public static readonly StyledProperty<int> GapProperty =
        AvaloniaProperty.Register<Poly, int>(nameof(Gap), 1, coerce: (poly, value) => value < 1 ? 1 : (value < ((Poly)poly).Points ? value : ((Poly)poly).Points));

    public static readonly StyledProperty<IBrush> FillProperty =
        AvaloniaProperty.Register<Poly, IBrush>(nameof(Fill), Brushes.Transparent);

    public static readonly StyledProperty<IBrush> StrokeProperty =
        AvaloniaProperty.Register<Poly, IBrush>(nameof(Stroke), Brushes.Black);

    public static readonly StyledProperty<double> StrokeThicknessProperty =
        AvaloniaProperty.Register<Poly, double>(nameof(StrokeThickness), 1.0);

    public int Points
    {
        get => GetValue(PointsProperty);
        set => SetValue(PointsProperty, value);
    }

    public int Gap
    {
        get => GetValue(GapProperty);
        set => SetValue(GapProperty, value);
    }

    public IBrush Fill
    {
        get => GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    public IBrush Stroke
    {
        get => GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    public double StrokeThickness
    {
        get => GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    static List<string> _invalidators = new List<string>
    {
        nameof(Points), nameof(Gap), nameof(Fill), nameof(Stroke), nameof(StrokeThickness)
    };
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (_invalidators.Contains(change.Property.Name))
        {
            InvalidateVisual();
        }
    }

    public override void Render(DrawingContext context)
    {
        // Get the size of the control
        var bounds = new Rect(Bounds.Size);
        double radius = Math.Min(bounds.Width, bounds.Height) / 2;

        // Calculate the center of the control
        var center = new Point(bounds.Width / 2, bounds.Height / 2);

        // Calculate the points on the polygon
        var polygonPoints = CalculatePolygonPoints(center, radius, Points);

        if (polygonPoints.Count < 3)
            return;

        var geometry = new StreamGeometry();

        using (var ctx = geometry.Open())
        {
            // Start from the first point
            int currentIndex = 0;
            ctx.BeginFigure(polygonPoints[currentIndex], true);

            // Move to each subsequent point based on the Gap
            do
            {
                currentIndex = (currentIndex + Gap) % Points;
                ctx.LineTo(polygonPoints[currentIndex]);
            } while (currentIndex != 0);

            ctx.EndFigure(true);
        }

        // Draw the polygon
        context.DrawGeometry(Fill, new Pen(Stroke, StrokeThickness), geometry);
    }

    private List<Point> CalculatePolygonPoints(Point center, double radius, int numPoints)
    {
        var points = new List<Point>();
        double angleStep = 2 * Math.PI / numPoints;

        for (int i = 0; i < numPoints; i++)
        {
            double angle = i * angleStep;
            double x = center.X + radius * Math.Cos(angle);
            double y = center.Y - radius * Math.Sin(angle); // Negative because Y axis is inverted in graphics
            points.Add(new Point(x, y));
        }

        return points;
    }
}