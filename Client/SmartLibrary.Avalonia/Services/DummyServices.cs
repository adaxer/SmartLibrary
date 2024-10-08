using SmartLibrary.Common.Interfaces;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Avalonia.Services;

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

public class DummyBookShareClient : IBookShareClient
{
    public Task<bool> ShareBook(SavedBook book)
    {
        // Simulate sharing the book and return true to indicate success
        return Task.FromResult(true);
    }
}



