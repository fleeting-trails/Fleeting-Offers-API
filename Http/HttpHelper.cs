using FleetingOffers.Attributes;
using FleetingOffers.Module.Auth;

namespace FleetingOffers.Http;

[ScopedService]
public class HttpHelper {
    public static TokenValidationResponse? GetAuthorizationPayload(HttpContext context) {
        return (TokenValidationResponse?) context.Items["AuthorizationPayload"];
    }
}