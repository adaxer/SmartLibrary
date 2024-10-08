﻿
using System.Net.Http.Json;

namespace SmartLibrary.Core.Services;

public class BookService : IBookService
{
    HttpClient _client;

    public BookService(HttpClient client)
    {
        _client = client;
    }

    public async Task<BookQuery> BookQueryAsync(string text)
    {
        var result = await _client.GetFromJsonAsync<BookQuery>($"books/v1/volumes?q={text}&maxResults=40");

        return result;

    }

    public async Task<Book> GetBookDetailsAsync(string id)
    {
        Book book = await _client.GetFromJsonAsync<Book>($"books/v1/volumes/{id}");

        return book;
    }
}
