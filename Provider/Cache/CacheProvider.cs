using System.Text.Json;
using FleetingOffers.Attributes;
using FleetingOffers.Settings;
using Microsoft.Extensions.Caching.Distributed;

namespace FleetingOffers.Provider;

[SingletonService]
public class CacheProvider
{
    private IDistributedCache _cache;

    public CacheProvider(IDistributedCache cache)
    {
        _cache = cache;
    }
    public string? GetString(string key)
    {
        var cachedItem = _cache.GetString(key);
        return cachedItem;
    }
    public void CreateString(string key, string value)
    {
        if (!CacheSettings.CacheEnabled)
        {
            _cache.SetString(
                    key,
                    value,
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = CacheSettings.DefaultExpiry }
                );
        }
    }
    public T GetOrCreate<T>(string key, Func<T> createItem)
    {
        if (!CacheSettings.CacheEnabled)
        {
            return createItem();
        }
        var cachedItem = _cache.GetString(key);
        if (cachedItem == null)
        {
            var item = createItem();

            _cache.SetString(
                key,
                JsonSerializer.Serialize(item),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = CacheSettings.DefaultExpiry }
            );
            return item;
        }
        else
        {
            return JsonSerializer.Deserialize<T>(cachedItem)!;
        }
    }
    public void Create(string key, object val)
    {
        if (CacheSettings.CacheEnabled)
        {
            _cache.SetString(
                key,
                JsonSerializer.Serialize(val),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = CacheSettings.DefaultExpiry }
            );
        }
    }
    public T? Get<T>(string key)
    {
        if (!CacheSettings.CacheEnabled)
        {
            return default;
        }
        var cachedItem = _cache.GetString(key);
        if (cachedItem == null)
        {
            return default;
        }
        return JsonSerializer.Deserialize<T>(cachedItem);
    }
}