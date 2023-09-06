namespace SmartLibrary.Common.Interfaces;

public interface IRestService
{
    Task<T> GetDataAsync<T>(string url) where T : class;
    Task<R> PostDataAsync<T,R>(string url, T payload) where T : class where R : class;
}
