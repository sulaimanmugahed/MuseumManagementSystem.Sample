namespace MuseumManagementSystem.Application.Constants;

public interface ICacheService
{
    Task SetToHash<T>(T data,string hash,string key);
    Task<bool> RemoveFromHash(string hash,string key);
    Task<T> GetFromHash<T>(string hash,string key);
    Task<bool> RemoveHash(string hash);
    Task<IEnumerable<T>> GetHash<T>(string hash);
    Task<bool> SetHash<T>(IEnumerable<(string key, T value)> data, string hash, TimeSpan expireTime);

}
