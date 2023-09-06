namespace SmartLibrary.Common.Interfaces;

public interface IBookService
{
    Task<BookQuery> BookQueryAsync(string text);

    Task<Book> GetBookDetailsAsync(string id);

    Task SaveBookAsync(SavedBook book);

    //Task UploadBookAsync(Book book, string notes);
}
