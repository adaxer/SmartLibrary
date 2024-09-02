namespace SmartLibrary.Common.ViewModels;

public partial class NewsViewModel : BaseViewModel, IRecipient<SharedBookMessage>, IRecipient<BookSavedMessage>
{
    private readonly INavigationService _navigator;
    private readonly IBookStorage _storage;

    public NewsViewModel(INavigationService navigator, IBookStorage storage, IPubSubService pubSubService)
    {
        Title = "Shared Pages";
        this._navigator = navigator;
        this._storage = storage;
        pubSubService.Subscribe<SharedBookMessage>(this);
        pubSubService.Subscribe<BookSavedMessage>(this);
        LoadDataAsync();
    }

    [ObservableProperty]
    ICollection<SavedBook> _savedBooks=new List<SavedBook>();

    [ObservableProperty]
    ICollection<SavedBook> _sharedBooks =new ObservableCollection<SavedBook>();

    public async void LoadDataAsync()
    {
        IsBusy = true;
        SavedBooks = new ObservableCollection<SavedBook>(await _storage.GetSavedBooks());
        IsBusy = false;
    }

    public void Receive(SharedBookMessage message)
    {
        SharedBooks.Add(message.Value);
    }

    public void Receive(BookSavedMessage message)
    {
        LoadDataAsync();
    }
}
