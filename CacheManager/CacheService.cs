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

        public async Task<List<TModel>> GetCacheValueAsync<TModel>(string key) where TModel: class
        {
            var data = await _database.StringGetAsync(key);
            if (data.IsNullOrEmpty)
            {
                return null;
            }    
            return JsonSerializer.Deserialize<List<TModel>>(data);
        }
    }
}
