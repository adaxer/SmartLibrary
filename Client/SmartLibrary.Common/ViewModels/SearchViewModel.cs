

using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Common.ViewModels;

public partial class SearchViewModel : BaseViewModel
{
    readonly IBookService bookService;
    private readonly INavigationService navigationService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SearchCommand))]
    string _searchText=string.Empty;

    [ObservableProperty]
    bool _isRefreshing;

    [ObservableProperty]
    ObservableCollection<Book> _books=new();

    [ObservableProperty]
    private Book? _currentBook;

    public SearchViewModel(IBookService service, INavigationService navigationService)
    {
        bookService = service;
        this.navigationService = navigationService;
        Title = "Suche";
    }

    [RelayCommand(CanExecute =nameof(CanSearch))]
    private Task SearchAsync() => LoadDataAsync();

    bool CanSearch => SearchText is string text && text.Length >2;

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
        //var items = await bookService.GetItems();

        //foreach (var item in items)
        //{
        //    Items.Add(item);
        //}
        return Task.CompletedTask;
    }

    private async Task LoadDataAsync()
    {
        IsBusy = true;
        try
        {
            var query = await bookService.BookQueryAsync(SearchText);
            Title = $"Suche ({query.Count} Treffer)";
            Books = new ObservableCollection<Book>(query.Books);
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
