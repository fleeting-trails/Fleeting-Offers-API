namespace FleetingOffers.Settings;

public class AutoMapperSettings {
    public AutoMapperSettings(WebApplicationBuilder builder) {
        builder.Services.AddAutoMapper(typeof(Program));
    }
}