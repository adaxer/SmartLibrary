namespace SmartLibrary.Common.Extensions;

public static class INavigationServiceExtensions
{
    public static async Task<(bool result, object dialog)> ShowDialogAsync(this INavigationService service, Type targetType,
        params (string key, object value)[] data)
    {
        try
        {
            var method = typeof(INavigationService).GetMethod(nameof(INavigationService.ShowDialogAsync));
            var genericMethod = method!.MakeGenericMethod(targetType);
            var task = (Task)genericMethod!.Invoke(service, new object[] { data })!;
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            var result = resultProperty?.GetValue(task)!;
            var item1 = result.GetType().GetField("Item1")!.GetValue(result);
            var item2 = result.GetType().GetField("Item2")!.GetValue(result);
            return ((bool)item1!, item2)!;
        }
        catch (Exception ex)
        {
            Trace.TraceError($"Error showing Dialog: {ex}");
            return (false, ex);
        }
    }
    public static async Task NavigateAsync(this INavigationService service, Type targetType,
        params (string key, object value)[] data)
    {
        try
        {
            var method = typeof(INavigationService).GetMethod(nameof(INavigationService.NavigateAsync));
            var genericMethod = method!.MakeGenericMethod(targetType);
            var task = (Task)genericMethod!.Invoke(service, new object[] { data })!;
            await task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Trace.TraceError($"Error showing Page: {ex}");
        }
    }
}
