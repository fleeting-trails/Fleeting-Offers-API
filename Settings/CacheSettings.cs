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
        // Connection = builder.Configuration.GetConnectionString("Redis");
        Connection = builder.Configuration["Redis:ConnectionString"];
        Console.WriteLine("Redis Connection String: " + Connection);
        CacheStorage = builder.Configuration["CacheSettings.CacheStorage"];
        CacheEnabled = builder.Configuration.GetValue("CacheSettings:CacheEnabled", false);
        CacheKey = builder.Configuration["CacheSettings.CacheKey"];

        if (CacheEnabled)
        { 

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(Connection!));

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Connection;
                options.InstanceName = CacheKey;
            });
        }
    }
}