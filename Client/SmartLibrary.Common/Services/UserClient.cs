using Microsoft.Extensions.Logging;
using SmartLibrary.Common.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SmartLibrary.Common.Services;

public partial class UserClient : ObservableObject, IUserClient
{
    const string StorageKey = "UserInfo";
    private readonly HttpClient _client;
    private readonly ILogger<UserClient> _logger;
    private readonly ISecureStorage _secureStorage;
    private readonly IPubSubService _pubSubService;
    private static UserInfo s_userInfo = UserInfo.None;

    public UserClient(HttpClient client, ILogger<UserClient> logger, ISecureStorage secureStorage, IPubSubService pubSubService)
    {
        _client = client;
        _logger = logger;
        _secureStorage = secureStorage;
        _pubSubService = pubSubService;
    }

    [ObservableProperty]
    private UserInfo _userInfo = s_userInfo;

    partial void OnUserInfoChanged(UserInfo value)
    {
        s_userInfo = value;
        _pubSubService.Publish(new UserInfoChangedMessage(value));
    }

    public string? UserName => UserInfo.UserName;

    public string? Email => UserInfo.Email;

    public void Logout()
    {
        _secureStorage.Remove(StorageKey);
        UserInfo = UserInfo.None;
    }

    public async Task<bool> GetIsLoggedInAsync()
    {
        if (UserInfo == UserInfo.None)
        {
            if (await TryLoadUserInfoAsync() == false)
            {
                return false;
            }
        }
        return true;

        // Todo: Ping server with access token resp Refreshtoken
    }

    public async Task<bool> LoginAsync(string userName, string email, string password)
    {
        try
        {
            var url = $"auth/login";
            var response = await _client.PostAsJsonAsync(url, new LoginInfo(userName, email, password));
            var success = await TryGetUserInfoAsync(response);
            return success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed");
            return false;
        }
    }

    private async Task<bool> TryGetUserInfoAsync(HttpResponseMessage response)
    {
        try
        {
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse!.AccessToken);
            UserInfo = (await _client.GetFromJsonAsync<UserInfo>("auth/userinfo"))!;
            UserInfo.AccessToken = tokenResponse.AccessToken;

            await SaveUserInfoAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task SaveUserInfoAsync()
    {
        await _secureStorage.SetAsync(StorageKey, UserInfo);
    }

    private async Task<bool> TryLoadUserInfoAsync()
    {
        UserInfo = await _secureStorage.GetAsync(StorageKey, UserInfo.None);

        return UserInfo != UserInfo.None;
    }

    public record LoginInfo(string userName, string email, string password);
}
