using Microsoft.Extensions.Caching.Memory;
using Prometheus;
using WsRest_UpWay.Models.Cache;

namespace WsRest_UpWay;

public class MetricsService(ICache cache) : BackgroundService
{
    private readonly Gauge cacheSizeMetric = Metrics.CreateGauge("cache_size_total", "Number of bytes the items in the cache takes.");
    private readonly Gauge cacheTotalHits = Metrics.CreateGauge("cache_total_hits", "How many times did cached item get used.");
    private readonly Gauge cacheTotalMisses = Metrics.CreateGauge("cache_total_misses", "How many times did we have to fetch an items before it gets cached.");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    ReadySetGoAsync(stoppingToken);
                }
                catch
                {
                    // Something failed? OK, whatever. We will just try again.
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
        }
    }

    private void ReadySetGoAsync(CancellationToken cancel)
    {
        MemoryCacheStatistics? statistics = cache.Statistics;
        if(statistics == null) return;
        
        cacheSizeMetric.Set((double)statistics.CurrentEstimatedSize!);
        cacheTotalHits.Set(statistics.TotalHits);
        cacheTotalMisses.Set(statistics.TotalMisses);
    }
}