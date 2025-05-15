namespace FleetingOffers.Settings;

using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

public class DatabaseSettings
{
    public static string? ConnectionString;
    public DatabaseSettings(WebApplicationBuilder builder) {
        // ConnectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        ConnectionString = builder.Configuration["Database:ConnectionString"];
        // ConnectionString = "Host=localhost:5432;Username=fleetingtrails;Password=StudentPortal123!;Database=fleetingtrails";
        // ConnectionString = "Host=postgres:5432;Username=fleetingtrails;Password=StudentPortal123!;Database=fleetingtrails";
        // builder.Services.AddSqlite<AppDbContext>(ConnectionString);
        Console.WriteLine("Database Connection String: " + ConnectionString);
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(ConnectionString));
    }
}
