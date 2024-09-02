using CommunityToolkit.Mvvm.Messaging;
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.MAUI.Services;
public class PubSubService : IPubSubService
{
    public void Publish<T>(T message) where T : class
    {
        WeakReferenceMessenger.Default.Send<T>(message);
    }

    public void Subscribe<T>(object subscriber) where T:class
    {
        if (subscriber is IRecipient<T> recipient)
        {
            MessageHandler<object, T> handler = new MessageHandler<object, T>((r, m) =>
            {
                if (Application.Current!.Dispatcher.IsDispatchRequired)
                {
                    Application.Current.Dispatcher.Dispatch(() => (r as IRecipient<T>)!.Receive(m));
                }
                else
                {
                    (r as IRecipient<T>)!.Receive(m);
                }
            });
            WeakReferenceMessenger.Default.Register(recipient, handler);
        }
        else
        {
            throw new ArgumentException("Must be an IRecipient<T>");
        }
    }

    public void Unsubscribe<T>(object subscriber) where T:class
    {
        if (subscriber is IRecipient<T> recipient)
        {
            WeakReferenceMessenger.Default.Unregister<T>(recipient);
        }
        else
        {
            throw new ArgumentException("Must be an IRecipient<T>");
        }
    }
}
