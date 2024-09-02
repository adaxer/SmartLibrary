using Microsoft.AspNetCore.SignalR;

namespace SmartLibrary.Api;

public class BooksHub : Hub
{
    public Task ShareBook(string bookInfo)
    {
        return Clients.All.SendAsync("BookShared", bookInfo);
    }
}