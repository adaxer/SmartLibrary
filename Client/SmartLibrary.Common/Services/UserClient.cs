using Microsoft.Extensions.Logging;
using SmartLibrary.Common.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SmartLibrary.Common.Services;

public class UserClient : IUserClient
{
    const string StorageKey = "UserInfo";
    private readonly HttpClient _client;
    private readonly ILogger<UserClient> _logger;
    private readonly ISecureStorage _secureStorage;
    private static UserInfo _userInfo = UserInfo.None;

    public UserClient(HttpClient client, ILogger<UserClient> logger, ISecureStorage secureStorage)
    {
        _client = client;
        _logger = logger;
        _secureStorage = secureStorage;
    }

    public string? UserName => _userInfo?.UserName;

    public string? Email => _userInfo?.Email;

    public UserInfo? UserInfo => _userInfo;

    public void Logout()
    {
        _secureStorage.Remove(StorageKey);
        _userInfo = UserInfo.None;
    }

    public async Task<bool> GetIsLoggedInAsync()
    {
        if (_userInfo == UserInfo.None)
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
            _userInfo = (await _client.GetFromJsonAsync<UserInfo>("auth/userinfo"))!;
            _userInfo.AccessToken = tokenResponse.AccessToken;

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
        await _secureStorage.SetAsync(StorageKey, _userInfo);
    }

    private async Task<bool> TryLoadUserInfoAsync()
    {
        _userInfo = await _secureStorage.GetAsync(StorageKey, UserInfo.None);
        return _userInfo != UserInfo.None;
    }

    public record LoginInfo(string userName, string email, string password);
}
