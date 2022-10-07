using StackExchange.Redis;
using System.Text.Json;

namespace CacheManager
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IDatabase database)
        {
            _database = database;
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            await _database.StringSetAsync(key, value, TimeSpan.FromSeconds(30));
        }

        public async Task<List<TModel>> GetCacheValueAsync<TModel>(string key) where TModel : class
        {
            var data = await _database.StringGetAsync(key);
            if (data.IsNullOrEmpty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<List<TModel>>(data);
        }

        public async Task<List<TModel>> GetOrAdd<TModel>(string key, Func<Task<List<TModel>>> getFunction) where TModel : class
        {
            var redisValue = await _database.StringGetAsync(key);
            if (redisValue.IsNull)
            {
                var cacheEntry = getFunction();
                using (var modelstream = new MemoryStream())
                {
                    await JsonSerializer.SerializeAsync(modelstream, cacheEntry.Result);
                    await _database.StringSetAsync(key, modelstream.ToArray(), TimeSpan.FromSeconds(30));
                }
                return cacheEntry.Result;
            }
            return JsonSerializer.Deserialize<List<TModel>>(redisValue);
        }
    }
}
