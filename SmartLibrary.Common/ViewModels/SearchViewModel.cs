using CommunityToolkit.Mvvm.Messaging;
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Common.ViewModels;

public partial class SearchViewModel : BaseViewModel
{
    readonly IBookService bookService;
    private readonly INavigationService navigationService;

    [ObservableProperty]
    string searchText;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    ObservableCollection<Book> books;

    public SearchViewModel(IBookService service, INavigationService navigationService)
    {
        bookService = service;
        this.navigationService = navigationService;
        Title = "Suche";
    }

    [RelayCommand]
    private Task Search() => LoadDataAsync(); 

    [RelayCommand]
    private async void GoToDetails(Book book)
    {
        await navigationService.GoToAsync(nameof(DetailsViewModel), new Dictionary<string, object>
        {
            { "BookId", book.Id }
        });
    }

    [RelayCommand]
    private async void OnRefreshing()
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
            throw;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
