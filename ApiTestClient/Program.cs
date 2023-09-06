using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ApiTestClient;

class Program
{
    private static HubConnection _connection;

    static void Main(string[] args)
    {
        Console.ReadLine();
        CallSignalR().Wait();
        Console.ReadLine();
    }

    private static async Task CallSignalR()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("https://daxbookserver.azurewebsites.net/bookshub")
            //.WithUrl("https://localhost:7023/bookshub")
            .Build();

        _connection.On<string>("BookShared", Console.WriteLine);

        string message;
        await _connection.StartAsync();
        do
        {
            message = Console.ReadLine();
            await _connection.InvokeAsync("ShareBook", message);
        } while (!string.IsNullOrEmpty(message));
    }
}
