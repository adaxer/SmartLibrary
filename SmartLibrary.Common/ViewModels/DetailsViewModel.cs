using SmartLibrary.Common.Interfaces;
using System.Diagnostics;
using System.Windows.Input;

namespace SmartLibrary.Common.ViewModels;

public partial class DetailsViewModel : BaseViewModel
{
    private readonly IBookService _bookService;
    private readonly ILocationService _locationService;
    //private readonly IBookShareClient _bookShareClient;
    //private readonly IBookStorage _bookStorage;
    //private readonly IUserService _userService;

    public DetailsViewModel(INavigationService navigationService, IBookService bookService)//, IBookShareClient bookShareClient, IBookStorage bookStorage, ILocationService locationService, IUserService userService) 
    {
        _bookService = bookService;
        //_bookShareClient = bookShareClient;
        //_bookStorage = bookStorage;
        //_userService = userService;
        //_locationService = locationService;
        if (navigationService.NavigationParameters(nameof(DetailsViewModel)) is { } parameters)
        {
            if (parameters.TryGetValue("BookId", out var id))
            {
                LoadBook(id?.ToString());
            }
        }
    }

    private async void LoadBook(string id)
    {
        Title = "Hole Info für Book " + id;
        IsBusy = true;
        Book = await _bookService.GetBookDetailsAsync(id);
        Title = Book?.Info?.Title;
        IsBusy = false;
    }

    [ObservableProperty]
    Book book;

    //[RelayCommand]
    //private async Task Save()
    //{
    //    var location = new Location();
    //    try
    //    {
    //        location = await _locationService.GetLocationQuick();
    //    }
    //    catch (Exception)
    //    {
    //        Debug.WriteLine("Location not possible");
    //    }
    //    string notes = "Notizen";
    //    SavedBook savedBook = new SavedBook
    //    {
    //        BookId = Book?.Id,
    //        SaveDate = DateTimeOffset.Now,
    //        Title=Book?.Info?.Title,
    //        UserName = _userService.IsLoggedIn ? _userService.UserName : "somebody",
    //        Notes = notes,
    //        Location = location
    //    };
    //    await _bookStorage.SaveBook(savedBook);
    //    await _bookShareClient.ShareBook(savedBook);
    //}
}