
namespace SmartLibrary.MAUI.Views;
public class BasePage : ContentPage, IQueryAttributable
{
    public BasePage(BaseViewModel viewModel)
    {
        BindingContext = ViewModel = viewModel;
    }

    // This is necessary because the Shell calls ApplyQueryAttributes not only from Shell.GoToAsync, but also from INavigation.PushModal(),
    // which doesn't even support query parameters
    internal bool SuppressApplyQueryAttributes { private get; set; }

    public BaseViewModel ViewModel { get; private set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (SuppressApplyQueryAttributes)
        {
            return;
        }
        ViewModel.OnNavigatedTo(query);
    }

    internal async Task<bool> WaitForPopModalAsync()
    {
        var result = await ViewModel.ModalCompletionTask;
        await Shell.Current.Navigation.PopModalAsync(true);
        return result;
    }

    internal void ApplyQueryAttributesModal(IDictionary<string, object> query)
    {
        ViewModel.OnNavigatedToModal(query);
    }
}
