using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Projektanker.Icons.Avalonia.Models;
using Projektanker.Icons.Avalonia;

namespace SmartLibrary.Avalonia.Services;
public class CustomIconProvider : IIconProvider
{
    private const string _prefix = "custom";
    private readonly Dictionary<string, IconModel> _icons = new();
    private static readonly Regex _viewBoxRegex = new("viewBox=\"([0-9 -]+)\"");
    private static readonly Regex _pathRegex = new("<path d=\"(.+)\"");
    private static readonly Assembly _assembly = typeof(CustomIconProvider).Assembly;
    private static readonly string _resourceNameTemplate
        = $"{_assembly.GetName().Name}.Images.{{0}}.svg";

    public string Prefix => _prefix;

    public IconModel GetIcon(string value)
    {
        if (_icons.TryGetValue(value, out var icon))
        {
            return icon;
        }

        icon = GetIconFromResource(value);
        return _icons[value] = icon;
    }

    private static IconModel GetIconFromResource(string value)
    {
        using (Stream stream = GetIconResourceStream(value))
        using (TextReader textReader = new StreamReader(stream))
        {
            var svg = textReader.ReadToEnd();
            var viewBoxMath = _viewBoxRegex.Match(svg);
            var viewBox = viewBoxMath.Groups[1].Value;
            var pathMatch = _pathRegex.Match(svg);
            var path = pathMatch.Groups[1].Value;
            return new IconModel(
                ViewBoxModel.Parse(viewBox),
                new PathModel(path));
        }
    }

    private static Stream GetIconResourceStream(string value)
    {
        return TryGetIconResourceStream(value, out var stream)
            ? stream
            : throw new KeyNotFoundException($"Material Design Icon \"{value}\" not found!");
    }

    private static bool TryGetIconResourceStream(string value, out Stream stream)
    {
        stream = default;

        if (value.Length <= _prefix.Length + 1)
        {
            return false;
        }

        var withoutPrefix = value.Substring(_prefix.Length + 1);
        var resourceName = string.Format(_resourceNameTemplate, withoutPrefix);
        stream = _assembly.GetManifestResourceStream(resourceName);

        return stream != default;
    }
}
