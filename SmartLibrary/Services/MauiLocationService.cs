using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Services;
public class MauiLocationService : ILocationService
{
    public async Task<Common.Models.Location> GetLocationAsync()
    {
        var loc = await Geolocation.Default.GetLocationAsync();
        return new Common.Models.Location(loc.Latitude, loc.Longitude);
    }
}
