using CommunityToolkit.Mvvm.Messaging;

namespace SmartLibrary.Common.Services;
public class CheckItemService
{
    public async void Start()
    {
        while(true)
        {
            await Task.Delay(Random.Shared.Next(2000, 8000));
            var item = new SampleItem { Title = $"""Sample - created at {DateTime.Now.ToString("hh:mm.ss")}""", Description = $"A new item: {DateTime.Now.Ticks}" };
            Debug.WriteLine(item.Title);
            WeakReferenceMessenger.Default.Send(item);
        }
    }
}
