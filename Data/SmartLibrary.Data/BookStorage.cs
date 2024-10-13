﻿using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Models;

namespace SmartLibrary.Data;

public class BookStorage : IBookStorage
{
    private readonly BooksContext context;
    private readonly HttpClient _client;

    public BookStorage(BooksContext context, IConfiguration configuration)
    {
        this.context = context;
        _client = new HttpClient { BaseAddress = new Uri(configuration.GetValue<string>("ApiBaseUrl"), UriKind.Absolute) };
    }

    public async Task SaveBook(SavedBook book)
    {
        var existingBook = await context.SavedBooks.FindAsync(book.Id);
        if (existingBook == null)
        {
            context.SavedBooks.Add(book);
        }
        else
        {
            context.Entry(existingBook).CurrentValues.SetValues(book);
        }
        await context.SaveChangesAsync();
        await _client.PostAsJsonAsync<SavedBook>(_client.BaseAddress==null ? "https://localhost:7023" : "", book);
    }

    public async Task<IEnumerable<SavedBook>> GetSavedBooks()
    {
        return await context.SavedBooks.ToListAsync();
    }
}
