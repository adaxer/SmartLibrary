namespace SmartLibrary.Common.Services;

public class DummyLocationService : ILocationService
{
    public Task<Location> GetLocationAsync()
    {
        return Task.FromResult(new Location(36.618972, 27.840125)); // Schön für Work and Travel
    }
}

public class DummyBookStorage : IBookStorage
{
    private readonly List<SavedBook> _savedBooks = new();

    public Task<IEnumerable<SavedBook>> GetSavedBooks()
    {
        return Task.FromResult<IEnumerable<SavedBook>>(_savedBooks);
    }

    public Task SaveBook(SavedBook book)
    {
        _savedBooks.Add(book);
        return Task.CompletedTask;
    }
}

public class DummyStorage : ISecureStorage
{
    private readonly Dictionary<string, object> _storage = new();

    public Task SetAsync<T>(string key, T value)
    {
        _storage[key] = value!;
        return Task.CompletedTask;
    }

    public Task<T> GetAsync<T>(string key, T fallback = default!)
    {
        if (_storage.TryGetValue(key, out var value))
        {
            return Task.FromResult((T)value);
        }
        return Task.FromResult(fallback);
    }

    public void Remove(string key)
    {
        _storage.Remove(key);
    }
}



