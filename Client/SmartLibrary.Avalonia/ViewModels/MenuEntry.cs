using System;

namespace SmartLibrary.Avalonia.ViewModels;

public class MenuEntry
{
    public MenuEntry(string name, Type targetType, string icon, bool isModal=false)
    {
        Name = name;
        TargetType = targetType;
        Icon = icon.StartsWith("mdi-") ? icon : $"mdi-{icon}";
        IsModal = isModal;
    }

    public string Name { get; }

    public Type TargetType { get; }
    public string Icon { get; }
    public bool IsModal { get; }
}
