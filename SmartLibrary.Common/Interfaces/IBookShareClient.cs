namespace SmartLibrary.Common.Interfaces;

public interface IBookShareClient
{
    Task<bool> ShareBook(SavedBook book);
}
