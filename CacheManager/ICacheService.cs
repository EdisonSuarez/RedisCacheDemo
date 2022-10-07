namespace CacheManager
{
    public interface ICacheService
    {
        Task<List<TModel>> GetCacheValueAsync<TModel>(string key) where TModel : class;
        Task<List<TModel>> GetOrAdd<TModel>(string key, Func<Task<List<TModel>>> getFunction) where TModel : class;
        Task SetCacheValueAsync(string key, string value);
    }
}
