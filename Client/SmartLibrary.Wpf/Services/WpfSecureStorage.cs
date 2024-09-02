using System;
using System.IO.IsolatedStorage;
using System.IO;
using System.Threading.Tasks;
using SmartLibrary.Common.Interfaces;
using Newtonsoft.Json;

namespace SmartLibrary.Wpf.Services;
public class WpfSecureStorage : ISecureStorage
{
    public async Task<T> GetAsync<T>(string key, T fallback = default)
    {
        using (var isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
        {
            if (isoStore.FileExists(key))
            {
                using (var isoStream = new IsolatedStorageFileStream(key, FileMode.Open, isoStore))
                {
                    using (var reader = new StreamReader(isoStream))
                    {
                        string json = await reader.ReadToEndAsync();
                        return (json == null)
                            ? fallback
                            : JsonConvert.DeserializeObject<T>(json);
                    }
                }
            }
            return fallback;
        }
    }

    public void Remove(string key)
    {
        using (var isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
        {
            if (isoStore.FileExists(key))
            {
                isoStore.DeleteFile(key);
            }
        }
    }

    public async Task SetAsync<T>(string key, T value)
    {
        if (value != null)
        {
            using (var isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                using (var isoStream = new IsolatedStorageFileStream(key, FileMode.Create, isoStore))
                {
                    using (var writer = new StreamWriter(isoStream))
                    {
                        var data = JsonConvert.SerializeObject(value, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Include,
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                        await writer.WriteAsync(data);
                    }
                }
            }
        }
    }
}
