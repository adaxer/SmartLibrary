using System.Collections.Generic;
using System.Threading.Tasks;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Wpf.Interfaces;


public interface IDialogService
{
    Task<(bool result, T content)> ShowDialogAsync<T>(T content, IDictionary<string, object> data) where T : BaseViewModel;
}
