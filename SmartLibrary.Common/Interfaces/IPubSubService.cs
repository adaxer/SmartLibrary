using SmartLibrary.Common.Messages;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Common.Interfaces;
public interface IPubSubService
{
    void Publish<T>(T message) where T:class;
    void Subscribe<T>(object subscriber) where T:class;
    void Unsubscribe<T>(object subscriber) where T : class;
}
