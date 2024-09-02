namespace SmartLibrary.Core.Interfaces;

public interface IBookService
{
    Task<BookQuery> BookQueryAsync(string text);

    Task<Book> GetBookDetailsAsync(string id);


    //Task UploadBookAsync(Book book, string notes);
}
