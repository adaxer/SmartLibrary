using Microsoft.EntityFrameworkCore;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Models;

namespace SmartLibrary.Data;

public class BookStorage : IBookStorage
{
    private readonly BooksContext context;

    public BookStorage(BooksContext context)
    {
        this.context = context;

    }

    public async Task SaveBook(SavedBook book)
    {
        var existingBook = await context.SavedBooks.FindAsync(book.BookId);
        if (existingBook == null)
        {
            context.SavedBooks.Add(book);
        }
        else
        {
            context.Entry(existingBook).CurrentValues.SetValues(book);
        }
        //pubSubService.Publish(new BookSavedMessage(book));
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SavedBook>> GetSavedBooks()
    {
        return await context.SavedBooks.ToListAsync();
    }
}
