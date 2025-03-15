namespace FleetingOffers.Settings;

public class JWTSettings
{
    public static string? Issuer { get; set; }
    public static string? Audience { get; set; }
    public static string? Secret { get; set; }
    public static int ExpireInHour { get; set;}
    public JWTSettings(WebApplicationBuilder builder) {
        Issuer = builder.Configuration["JwtToken:Issuer"];
        Audience = builder.Configuration["JwtToken:Audience"];
        Secret = builder.Configuration["JwtToken:Secret"];
        ExpireInHour = 48;
    }
}
