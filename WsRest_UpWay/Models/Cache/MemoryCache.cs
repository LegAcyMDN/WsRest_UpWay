using System.Collections;
using Microsoft.Extensions.Caching.Memory;
using Prometheus;

namespace WsRest_UpWay.Models.Cache;

public class MemoryCache : ICache
{
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;

    public MemoryCache(IMemoryCache cache, IConfiguration configuration)
    {
        _cache = cache;
        _configuration = configuration;
    }


    public Task<TItem?> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> factory)
    {
        return _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetSlidingExpiration(TimeUtils.PrettyParse(_configuration["CACHE_SLIDING_EXPIRATION"] ?? "5m"))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(_configuration["CACHE_ABSOLUTE_EXPIRATION"] ?? "10m"));

            var item = await factory.Invoke();

            if (item != null)
            {
                if (item is ISizedEntity)
                {
                    entry.SetSize(((ISizedEntity)item).GetSize());
                }
                else if (item is IEnumerable)
                {
                    var items = (IEnumerable)item;
                    long size = 0;

                    foreach (var i in items)
                        if (i is ISizedEntity)
                            size += ((ISizedEntity)i).GetSize();

                    entry.SetSize(size);
                }
                else
                {
                    Console.WriteLine("WARNING: cached object " + item.GetType().Name +
                                      " does not implement ISizedEntity");
                    entry.SetSize(0);
                }
            }

            return item;
        });
    }

    public MemoryCacheStatistics? Statistics => this._cache.GetCurrentStatistics();
}