namespace SmartLibrary.Common.Interfaces;

public interface ISecureStorage
{
    Task SetAsync<T>(string key, T value);

    Task<T> GetAsync<T>(string key, T fallback = default(T)!);

    void Remove(string key);
}
