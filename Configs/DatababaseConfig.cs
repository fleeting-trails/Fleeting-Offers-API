namespace FleetingOffers.Configs;

public class DatabaseConfig
{
    public readonly string ConnectionString = "";

    public static void Initialize(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        builder.Services.AddSqlite<AppDbContext>(connectionString);
    }
}