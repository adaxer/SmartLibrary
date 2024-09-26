namespace SmartLibrary.Core.Interfaces;

public interface IBookService
{
    Task<BookQuery> BookQueryAsync(string text, int pageSize=10, int page=1);

    Task<Book> GetBookDetailsAsync(string id);


    //Task UploadBookAsync(Book book, string notes);
}
