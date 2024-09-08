using System;
using System.Threading.Tasks;
using NSubstitute;
using SmartLibrary.Avalonia.Services;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Models;
using SmartLibrary.Common.Services;
using SmartLibrary.Common.ViewModels;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddSingleton<INavigationService, AvaloniaNavigationService>();
        services.AddSingleton<IDialogService, AvaloniaDialogService>();
        services.AddTransient<IUserClient, UserClient>();
        services.AddHttpClient<IUserClient, UserClient>(client => client.BaseAddress = new Uri("https://localhost:7023", UriKind.Absolute));
      
        var mock = Substitute.For<ISecureStorage>();
        mock.GetAsync<UserInfo>(Arg.Any<string>(), Arg.Any<UserInfo>())
            .Returns(callInfo => Task.FromResult(callInfo.Arg<UserInfo>()));
        services.AddSingleton<ISecureStorage>(mock);
    }
}
