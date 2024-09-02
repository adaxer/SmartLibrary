using ISecureStorage = SmartLibrary.Common.Interfaces.ISecureStorage;

namespace SmartLibrary.MAUI.Services;
internal class MAUISecureStorage : ISecureStorage
{
    public async Task<T> GetAsync<T>(string key, T fallback = default!)
    {
        var json = await SecureStorage.GetAsync(key);
        return (json == null)
            ? fallback
            : JsonConvert.DeserializeObject<T>(json)!;
    }

    public void Remove(string key)
    {
        SecureStorage.Remove(key);
    }

    public async Task SetAsync<T>(string key, T value)
    {
        if (value != null)
        {
            await SecureStorage.SetAsync(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }
    }
}
