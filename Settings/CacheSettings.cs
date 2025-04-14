using StackExchange.Redis;

namespace FleetingOffers.Settings;

public class CacheSettings
{
    public static string? Connection;
    public static readonly TimeSpan DefaultExpiry = TimeSpan.FromHours(1);
    public static string CacheStorage;
    public static bool CacheEnabled;
    public static string CacheKey;

    public CacheSettings(WebApplicationBuilder builder)
    {
        Connection = builder.Configuration.GetConnectionString("Redis");
        CacheStorage = builder.Configuration["CacheSettings.CacheStorage"];
        CacheEnabled = builder.Configuration.GetValue("CacheSettings:CacheEnabled", false);
        CacheKey = builder.Configuration["CacheSettings.CacheKey"];

        if (CacheEnabled)
        {

            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Connection!)); // Replace with your Redis server details

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Connection;
                options.InstanceName = CacheKey;
            });
        }
    }
}