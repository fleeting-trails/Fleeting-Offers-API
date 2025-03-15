using FleetingOffers.Http;
using FleetingOffers.Module.Auth;
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
    public IActionResult GetOtp(GetOtpAdminPayload body) {
        try {
            var email = body.Email;
            var Otp = _service.GetOtp(email);
            return AppHttpResponse.Ok("OTP Successfully sent to your email");
        } catch (Exception e) {
            return AppHttpResponse.BadRequest(e.Message);
        }
    }

    [HttpPost("set-password")]
    public IActionResult SetPassword(SetPasswordAdminPayload body) {
        try {
            _service.SetPassword(body.Email, body.Password, body.Otp);
            return AppHttpResponse.Ok("Password Set, Please login with your new password");
        } catch (Exception e) {
            return AppHttpResponse.BadRequest($"Failed to set password: {e.Message}");
        }
    }

    [HttpPost("login")]
    public IActionResult Login(LoginPayloadAdminDto body)
    {
        try {
            var res = _service.Login(body.Email, body.Password, body.Device);
            return AppHttpResponse.Ok(res, "Login Successful");
        } catch (Exception e) {
            return AppHttpResponse.BadRequest(e.Message);
        }
    }

}