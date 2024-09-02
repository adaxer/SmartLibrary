
using SmartLibrary.Common.Models;

namespace SmartLibrary.Common.ViewModels;

public partial class DetailsViewModel : BaseViewModel
{
    private readonly IBookService _bookService;
    private readonly ILocationService _locationService;
    private readonly IBookShareClient _bookShareClient;
    private readonly IBookStorage _bookStorage;
    private readonly IUserClient _userService;

    public DetailsViewModel(INavigationService navigationService, IBookService bookService, 
        ILocationService locationService, IUserClient userService, IBookStorage bookStorage, IBookShareClient bookShareClient) 
    {
        _bookService = bookService;
        _bookShareClient = bookShareClient;
        _bookStorage = bookStorage;
        _userService = userService;
        _locationService = locationService;
    }

    [ObservableProperty]
    Book? _book;

    [ObservableProperty]
    bool _canShare = false;

    [RelayCommand]
    private async Task SaveAsync()
    {
        if(_userService.UserInfo == UserInfo.None) return;
        var location = new Location(0,0);
        try
        {
            location = await _locationService.GetLocationAsync();
        }
        catch (Exception ex)
        {
            Trace.TraceError($"Location not possible ({ex})");
        }
        string notes = "Notizen";
        SavedBook savedBook = new SavedBook
        {
            BookId = Book?.Id,
            SaveDate = DateTimeOffset.Now,
            Title = Book?.Info?.Title,
            UserName = _userService.UserName,
            Notes = notes,
            Location = location
        };
        await _bookStorage.SaveBook(savedBook);
        await _bookShareClient.ShareBook(savedBook);
    }

    public override async void OnNavigatedTo(IDictionary<string, object> data)
    {
        base.OnNavigatedTo(data);
        CanShare = await _userService.GetIsLoggedInAsync();
        if (data.TryGetValue("BookId", out var bookId) && bookId is string id)
        {
            LoadBookAsync(id);
        }
    }

    private async void LoadBookAsync(string id)
    {
        Title = "Hole Info für Book " + id;
        IsBusy = true;
        Book = await _bookService.GetBookDetailsAsync(id);
        Title = Book!.Info!.Title;
        IsBusy = false;
    }
}
