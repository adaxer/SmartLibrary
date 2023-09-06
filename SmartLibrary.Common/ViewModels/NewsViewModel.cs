using CommunityToolkit.Mvvm.Messaging;
using SmartLibrary.Common.Messages;

namespace SmartLibrary.Common.ViewModels;

public partial class NewsViewModel : BaseViewModel, IRecipient<SharedBookMessage>, IRecipient<BookSavedMessage>
{
    private readonly INavigationService navigator;
    private readonly IBookStorage storage;

    public NewsViewModel(INavigationService navigator, IBookStorage storage, IPubSubService pubSubService)
    {
        Title = "Shared Pages";
        this.navigator = navigator;
        this.storage = storage;
        pubSubService.Subscribe<SharedBookMessage>(this);
        pubSubService.Subscribe<BookSavedMessage>(this);
        LoadData();
    }

    [ObservableProperty]
    ICollection<SavedBook> savedBooks;

    [ObservableProperty]
    ICollection<SavedBook> sharedBooks =new ObservableCollection<SavedBook>();

    public async void LoadData()
    {
        IsBusy = true;
        SavedBooks = new ObservableCollection<SavedBook>(await storage.GetSavedBooks());
        IsBusy = false;
    }

    public void Receive(SharedBookMessage message)
    {
        SharedBooks.Add(message.Value);
    }

    public void Receive(BookSavedMessage message)
    {
        LoadData();
    }
}
