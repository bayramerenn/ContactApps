using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.CacheService
{
    public class RedisCache : IRedisCache
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IServer? _server;

        public RedisCache(string host)
        {
            var options = ConfigurationOptions.Parse(host);
            options.ConnectRetry = 5;
            options.AllowAdmin = true;

            _connectionMultiplexer = ConnectionMultiplexer.Connect(options);

            var endpoints = _connectionMultiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                _server = _connectionMultiplexer.GetServer(endpoint);
            }
        }

        public ConnectionMultiplexer Connect()
        {
            return _connectionMultiplexer;
        }

        public IDatabase GetDb(int db = 0) => _connectionMultiplexer.GetDatabase(db);

        public async Task<bool> SaveAsync<T>(string key, T data, TimeSpan? expiry, int db = 0)
        {
            var value = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            });
            return await GetDb(db).StringSetAsync(key, value, expiry);
        }

        public async Task<T?> GetAsync<T>(string key, int db = 0)
        {
            RedisValue redisValue = await GetDb(db).StringGetAsync(key);

            if (redisValue.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T?>(redisValue.ToString(), new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                IncludeFields = true
            });
        }

        public async Task RemoveAsync(string key, int db = 0)
        {
            await GetDb(db).KeyDeleteAsync(key, CommandFlags.FireAndForget);
        }

        public async Task RemovePatternAsync(string pattern, int db = 0)
        {
            var keys = _server!.Keys(db, pattern: pattern).ToArray();

            await GetDb(db).KeyDeleteAsync(keys, CommandFlags.FireAndForget);
        }
    }
}