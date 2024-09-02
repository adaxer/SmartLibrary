using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SmartLibrary.Api;
using SmartLibrary.Core.Models;

namespace SmartLibrary.Api.Endpoints.Books;

public static class Save
{
    public static void MapSaveBookEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/books/save", SaveBookAsync);
    }

    private static async Task<IResult> SaveBookAsync(SavedBook book, IHubContext<BooksHub> context)
    {
        var json = JsonConvert.SerializeObject(book);
        await context.Clients.All.SendAsync("BookShared", json);
        return Results.Ok();
    }
}
