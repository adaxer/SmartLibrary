namespace SmartLibrary.Common.ViewModels;

public partial class NewsViewModel : BaseViewModel, IRecipient<SharedBookMessage>, IRecipient<BookSavedMessage>
{
    private readonly INavigationService _navigator;
    private readonly IBookStorage _storage;
    private readonly IBookShareClient _bookShareClient;

    public NewsViewModel(INavigationService navigator, IBookStorage storage, IPubSubService pubSubService, IBookShareClient bookShareClient)
    {
        Title = "Shared Pages";
        this._navigator = navigator;
        this._storage = storage;
        _bookShareClient = bookShareClient;
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
        SharedBooks = new ObservableCollection<SavedBook>(_bookShareClient.SharedBooks);
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
