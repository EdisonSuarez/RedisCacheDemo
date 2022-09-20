namespace CacheManager
{
    public interface ICacheService
    {
        Task<List<TModel>> GetCacheValueAsync<TModel>(string key) where TModel: class;
        Task SetCacheValueAsync(string key, string value);  
    }
}
