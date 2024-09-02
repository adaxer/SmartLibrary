using CommunityToolkit.Mvvm.Messaging;
using SmartLibrary.Common.Interfaces;
using System;
using System.Windows;

namespace SmartLibrary.Wpf.Services;

public class WpfPubSubService : IPubSubService
{
    public void Publish<T>(T message) where T : class
    {
        WeakReferenceMessenger.Default.Send(message);
    }

    public void Subscribe<T>(object subscriber) where T : class
    {
        if (subscriber is IRecipient<T> recipient)
        {
            var handler = new MessageHandler<object, T>((r, m) =>
            {
                if (!Application.Current.Dispatcher.CheckAccess())
                {
                    Application.Current.Dispatcher.Invoke(() => (r as IRecipient<T>).Receive(m));
                }
                else
                {
                    (r as IRecipient<T>).Receive(m);
                }
            });
            WeakReferenceMessenger.Default.Register(recipient, handler);
        }
        else
        {
            throw new ArgumentException("Must be an IRecipient<T>");
        }
    }

    public void Unsubscribe<T>(object subscriber) where T : class
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
