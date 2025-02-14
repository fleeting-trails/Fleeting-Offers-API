namespace FleetingOffers.Settings;

public class DatabaseSettings
{
    public static string? ConnectionString;
    public DatabaseSettings(WebApplicationBuilder builder) {
        ConnectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        builder.Services.AddSqlite<AppDbContext>(ConnectionString);
    }
}