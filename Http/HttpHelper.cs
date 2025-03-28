using FleetingOffers.Attributes;
using FleetingOffers.Module.Auth;
using FleetingOffers.Module.User;

namespace FleetingOffers.Http;

[ScopedService]
public class HttpHelper {
    public static HttpPayloadDto GetAuthorizationPayload(HttpContext context) {
        
        var res =  (TokenValidationResponse?)context.Items["AuthenticationResponse"];
        if (res != null && res.Token == null) throw new Exception("Unauthorized: Access Denied");
        if (res == null || res.Role == null || res.UserId == null) throw new Exception("NO_TOKEN: Failed to Process Authorization");
        return new HttpPayloadDto(res.Token!, res.UserId, (USER_ROLE)res.Role, res.Device, res.IsValid);
    }
}