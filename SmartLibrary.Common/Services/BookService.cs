
namespace SmartLibrary.Common.Services;

public class BookService : IBookService
{
    IRestService _rest;

    public BookService(IRestService rest)
    {
        _rest = rest;
    }

    public async Task<BookQuery> BookQueryAsync(string text)
    {
        var result = await _rest.GetDataAsync<BookQuery>
            (string.Format("https://www.googleapis.com/books/v1/volumes?q={0}&maxResults=40", text));

        return result;

    }

    public async Task<Book> GetBookDetailsAsync(string id)
    {
        Book book = await _rest.GetDataAsync<Book>(string.Format("https://www.googleapis.com/books/v1/volumes/{0}", id));

        return book;
    }

    public async Task SaveBookAsync(SavedBook book)
    {
        await _rest.PostDataAsync<SavedBook, SavedBook>("", book);
    }
}
