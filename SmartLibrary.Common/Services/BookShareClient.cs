using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.SignalR.Client;
using SmartLibrary.Common.Messages;

namespace SmartLibrary.Common.Services;

public class BookShareClient : IBookShareClient, IRequireInitializeAsync
{
    private HubConnection _connection;
    private readonly IPubSubService pubSubService;

    public BookShareClient(IPubSubService pubSubService)
    {
        this.pubSubService = pubSubService;
    }
    public async Task InitializeAsync()
    {
        try
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"https://daxbookserver.azurewebsites.net/bookshub")
                .Build();

            _connection.On<string>("BookShared", OnBookShared);
            await _connection.StartAsync();
            await Task.Delay(1000);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            throw;
        }
    }

    private void OnBookShared(string json)
    {
        var book = JsonConvert.DeserializeObject<SavedBook>(json);
        pubSubService.Publish(new SharedBookMessage(book));
    }

    public async Task<bool> ShareBook(SavedBook book)
    {
        try
        {
            if(_connection==null || _connection.State == HubConnectionState.Disconnected)
            {
                await InitializeAsync();
            }
            await _connection.InvokeAsync("ShareBook", JsonConvert.SerializeObject(book));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
