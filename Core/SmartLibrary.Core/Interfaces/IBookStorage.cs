﻿namespace SmartLibrary.Core.Interfaces;

public interface IBookStorage
{
    Task<IEnumerable<SavedBook>> GetSavedBooks();
    Task SaveBook(SavedBook book);
}
