using FleetingOffers.Module.Auth;
using FleetingOffers.Module.User;

namespace FleetingOffers.Settings;

public class AutoMapperSettings {
    public AutoMapperSettings(WebApplicationBuilder builder) {
        builder.Services.AddAutoMapper(typeof(Program));
        // builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}