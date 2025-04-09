using Microsoft.Extensions.Caching.Memory;

namespace WsRest_UpWay.Models.Cache;

public interface ICache
{
    public Task<TItem?> GetOrCreateAsync<TItem>(
        string key,
        Func<Task<TItem>> factory);
    
    public MemoryCacheStatistics? Statistics { get; }
}