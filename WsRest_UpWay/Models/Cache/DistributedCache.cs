using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace WsRest_UpWay.Models.Cache;

public class DistributedCache : ICache
{
    private readonly IDistributedCache _distributedCache;

    public DistributedCache(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<TItem?> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> factory)
    {
        var item = await _distributedCache.GetStringAsync(key);
        if (item == null)
        {
            var item2 = await factory.Invoke();
            var json = JsonSerializer.Serialize(item2);
            await _distributedCache.SetStringAsync(key, json);
            return item2;
        }

        return JsonSerializer.Deserialize<TItem>(item);
    }
}