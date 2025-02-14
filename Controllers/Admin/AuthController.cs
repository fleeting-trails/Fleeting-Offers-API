using FleetingOffers.Http;
using FleetingOffers.Modules.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controllers;

[Route("auth")]
[ApiController]
public class AuthControllers : ControllerBase {
    private readonly AuthService _service;
    public AuthControllers (AuthService service) {
        _service = service;
    }
    [HttpPost("get-otp")]
    public IResult GetOtp(GetOtpAdminPayload body) {
        try {
            var email = body.Email;
            var Otp = _service.GetOtp(email);
            return AppHttpResponse.Ok("OTP Successfully sent to your email");
        } catch (Exception e) {
            return AppHttpResponse.BadRequest(e.Message);
        }
    }
}