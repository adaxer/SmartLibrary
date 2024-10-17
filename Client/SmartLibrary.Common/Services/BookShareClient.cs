using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace SmartLibrary.Common.Services;

public class BookShareClient : IBookShareClient, IRequireInitializeAsync
{
    private HubConnection _connection = default!;
    private string _apiBaseUrl;
    private readonly IPubSubService pubSubService;
    private List<SavedBook> _sharedBooks=new();

    public IEnumerable<SavedBook> SharedBooks => _sharedBooks;

    public BookShareClient(IPubSubService pubSubService, IConfiguration configuration)
    {
        this.pubSubService = pubSubService;
        _apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl")!;
    }
    public async Task InitializeAsync()
    {
        try
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"{_apiBaseUrl}/bookshub")
                .WithAutomaticReconnect()
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
        try
        {
            var book = JsonConvert.DeserializeObject<SavedBook>(json)!;
            _sharedBooks.Add(book);
            pubSubService.Publish(new SharedBookMessage(book));
        }
        catch (Exception ex)
        {
            Trace.TraceError($"Error deserializing {json} into Sharedbook: {ex}");
        }
    }

    public async Task<bool> ShareBook(SavedBook book)
    {
        try
        {
            if (_connection == null || _connection.State == HubConnectionState.Disconnected)
            {
                await InitializeAsync();
            }
            await _connection!.InvokeAsync("ShareBook", JsonConvert.SerializeObject(book));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
