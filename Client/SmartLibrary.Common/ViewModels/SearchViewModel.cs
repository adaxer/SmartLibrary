using SmartLibrary.Common.Extensions;
using SmartLibrary.Common.Messages;

namespace SmartLibrary.Common.ViewModels;

public partial class SearchViewModel : BaseViewModel
{
    private const int PageSize = 5;
    private int _currentPage = 1;


    readonly IBookService bookService;
    private readonly INavigationService navigationService;
    private readonly IPubSubService _pubsub;
    private readonly ILocalizationService _localizationService;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SearchCommand))]
    string _searchText = string.Empty;

    [ObservableProperty]
    bool _isRefreshing;

    [ObservableProperty]
    ObservableCollection<Book> _books = new();

    [ObservableProperty]
    private Book? _currentBook;

    [ObservableProperty]
    private int _pageCount = 0;

    public SearchViewModel(IBookService service, INavigationService navigationService, IPubSubService pubSub, ILocalizationService localizationService)
    {
        bookService = service;
        this.navigationService = navigationService;
        _pubsub = pubSub;
        _localizationService = localizationService;
        Title = _localizationService.Get("Search");
    }

    [RelayCommand(CanExecute = nameof(CanSearch))]
    private Task SearchAsync() => LoadDataAsync();

    bool CanSearch => SearchText is string text && text.Length > 2;

    [RelayCommand]
    private async Task GoToDetailsAsync(Book book)
    {
        await navigationService.NavigateAsync<DetailsViewModel>(("BookId", book.Id));
    }

    partial void OnCurrentBookChanged(Book? oldValue, Book? newValue)
    {
        if (newValue is Book book)
        {
            GoToDetailsCommand.Execute(book);
        }
    }

    [RelayCommand]
    private async Task RefreshingAsync()
    {
        IsRefreshing = true;

        try
        {
            await LoadDataAsync();
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    public Task LoadMore()
    {
        if (_currentPage + 1 < PageCount)
        {
            return LoadDataAsync(++_currentPage);
        }
        return Task.CompletedTask;
    }

private async Task LoadDataAsync(int page = 1)
{
    IsBusy = true;
    try
    {
        var query = await bookService.BookQueryAsync(SearchText, PageSize, page);
        PageCount = (query.Count / PageSize) + ((query.Count % PageSize == 0) ? 0 : 1);
        if (page == 1)
        {
            Books = new ObservableCollection<Book>(query.Books);
        }
        else
        {
            Books.AddRange(query.Books);
        }
        var message = string.Format(_localizationService.Get("SearchResult"), SearchText, query.Count, page, PageCount);
        _pubsub.Publish(new StatusMessage(message));
    }
    catch (Exception ex)
    {
        Trace.TraceError($"{ex}");
    }
    finally
    {
        IsBusy = false;
    }
}
}
