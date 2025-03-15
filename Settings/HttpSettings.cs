namespace FleetingOffers.Settings;

public class HttpSettings {
    private readonly string[] Allowedhosts;
    
    public HttpSettings(WebApplicationBuilder builder) {
        var allowed = builder.Configuration["JwtToken:Issuer"];
        Allowedhosts = allowed != null ? allowed.Split(",") : ["*"];
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", policy =>
            {
                policy.WithOrigins(Allowedhosts) // Specify allowed origins
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .SetIsOriginAllowedToAllowWildcardSubdomains();
            });
        });

    }
}