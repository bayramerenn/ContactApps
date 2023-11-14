using StackExchange.Redis;

namespace Shared.CacheService
{
    public interface IRedisCache
    {
        public ConnectionMultiplexer Connect();

        public IDatabase GetDb(int db = 0);

        Task<T?> GetAsync<T>(string key, int db = 0);

        Task RemoveAsync(string key, int db = 0);

        Task RemovePatternAsync(string pattern, int db = 0);

        Task<bool> SaveAsync<T>(string key, T data, TimeSpan? expiry, int db = 0);
    }
}