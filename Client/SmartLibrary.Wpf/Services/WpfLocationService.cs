using System;
using System.Threading.Tasks;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Core.Models;

namespace SmartLibrary.Wpf.Services;

public class WpfLocationService : ILocationService
{
    public Task<Location> GetLocationAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Location> GetLocationQuick()
    {
        throw new NotSupportedException("No location on wpf");
    }
}
