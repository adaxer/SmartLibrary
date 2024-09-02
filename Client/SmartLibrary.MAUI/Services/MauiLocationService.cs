using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.MAUI.Services;
public class MauiLocationService : ILocationService
{
    public async Task<SmartLibrary.Core.Models.Location> GetLocationAsync()
    {
        Location? loc = await Geolocation.Default.GetLocationAsync();
        return new SmartLibrary.Core.Models.Location(loc!.Latitude, loc!.Longitude);
    }
}
