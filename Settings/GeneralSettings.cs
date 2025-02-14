using System.Reflection;
using FleetingOffers.Attributes;
using FleetingOffers.Http;

namespace FleetingOffers.Settings;

public class GeneralSettings
{
    public GeneralSettings(WebApplicationBuilder builder)
    {
        RegisterAllScopedClasses(builder);
        RegisterAllSingletonClasses(builder);

        builder.Services
        .AddControllers(
            options =>
            {
                options.Conventions.Add(new RoutePrefixConvention("api"));
            }
            )
        .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });
    }
    private void RegisterAllScopedClasses(WebApplicationBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly(); // Replace with specific assembly if needed
        var typesWithAttribute = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<ScopedServiceAttribute>() != null && !t.IsAbstract && !t.IsInterface);

        foreach (var type in typesWithAttribute)
        {
            builder.Services.AddScoped(type);
        }
    }
    private void RegisterAllSingletonClasses(WebApplicationBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly(); // Replace with specific assembly if needed
        var typesWithAttribute = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<SingletonServiceAttribute>() != null && !t.IsAbstract && !t.IsInterface);

        foreach (var type in typesWithAttribute)
        {
            builder.Services.AddSingleton(type);
        }
    }
}