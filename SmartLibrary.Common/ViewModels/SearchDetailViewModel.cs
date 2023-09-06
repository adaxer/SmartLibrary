namespace SmartLibrary.Common.ViewModels;

//[QueryProperty(nameof(Item), "Item")]
public partial class SearchDetailViewModel : BaseViewModel
{
	[ObservableProperty]
	SampleItem item;
}
