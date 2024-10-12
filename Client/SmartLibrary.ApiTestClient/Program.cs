using Microsoft.AspNetCore.SignalR.Client;
using SmartLibrary.Common.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SmartLibrary.ApiTestClient;

class Program
{
    private static HubConnection _connection;
    //const string ApiBaseAddress = "https://localhost:7023";
    const string ApiBaseAddress = "https://smartlibraryapiservice.azurewebsites.net";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Hit enter to login");
        Console.ReadLine();
        var token = await LoginAsync();
        var info = await GetUserInfoAsync(token.AccessToken);
        Console.WriteLine($"""
Logged in as {info.UserName} ({info.Email} with roles {string.Join(", ", info.Roles)})

AccessToken: {token.AccessToken}
""");
        await CallSignalRAsync();
        Console.ReadLine();
    }

    private static async Task<UserInfo> GetUserInfoAsync(string accessToken)
    {
        using var client = new HttpClient { BaseAddress = new Uri(ApiBaseAddress) };
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        var response = await client.GetAsync("auth/userinfo");
        var userInfo = await response.Content.ReadFromJsonAsync<UserInfo>();
        return userInfo;
    }

    private static async Task<TokenResponse> LoginAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri(ApiBaseAddress) };
        var response = await client.PostAsJsonAsync("auth/login", new UserLoginData("alice@bob.com", "alice", "123Admin!"));
        return await response.Content.ReadFromJsonAsync<TokenResponse>();
    }

    private static async Task CallSignalRAsync()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl($"{ApiBaseAddress}/bookshub")
            .Build();

        _connection.On<string>("BookShared", Console.WriteLine);

        string message;
        await _connection.StartAsync();
        Console.WriteLine("Connection established. Enter messages or blank line...");
        do
        {
            message = Console.ReadLine();
            await _connection.InvokeAsync("ShareBook", message);
        } while (!string.IsNullOrEmpty(message));
    }
}

internal class UserLoginData
{
    public UserLoginData(string email, string userName, string password)
    {
        Email = email;
        UserName = userName;
        Password = password;
    }

    public string Email { get; }
    public string UserName { get; }
    public string Password { get; }
}
