using StackExchange.Redis;

namespace FleetingOffers.Settings;

public class CacheSettings {
    public static string? Connection;
    public static readonly TimeSpan DefaultExpiry = TimeSpan.FromHours(1);
    public static readonly string CacheStorage = "Redis";
    public static readonly Boolean CacheEnabled = false;
    public CacheSettings(WebApplicationBuilder builder) {
        Connection = builder.Configuration.GetConnectionString("Redis");
        builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Connection!)); // Replace with your Redis server details

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Connection;
            options.InstanceName = "FleetingOffers:";
        });
    }
}