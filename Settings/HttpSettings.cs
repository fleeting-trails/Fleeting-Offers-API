namespace FleetingOffers.Settings;

public class HttpSettings {
    public static string[] Allowedhosts;
    public const string AdminRoutePrefix = "admin";
    
    public HttpSettings(WebApplicationBuilder builder) {
        var allowed = builder.Configuration["JwtToken:Issuer"];
        Allowedhosts = allowed != null ? allowed.Split(",") : ["http://localhost:5173", "http://localhost:3000", "http://localhost:8080"];
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173", "http://localhost:3000", "http://localhost:8080")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

    }
}