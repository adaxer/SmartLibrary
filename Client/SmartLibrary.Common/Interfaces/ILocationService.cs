namespace SmartLibrary.Common.Interfaces;
public interface ILocationService
{
    Task<Location> GetLocationAsync();
}
