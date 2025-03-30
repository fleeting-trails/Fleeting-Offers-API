using System.Reflection;

namespace FleetingOffers.Settings;

public class DocumentationSettings
{
    public DocumentationSettings(WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer(); // Needed for minimal APIs
        builder.Services.AddSwaggerGen(options =>
        {
            // ✅ Include XML docs
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}