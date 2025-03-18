namespace FleetingOffers.Settings;

public class DocumentationSettings
{
    public DocumentationSettings(WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
    }
}