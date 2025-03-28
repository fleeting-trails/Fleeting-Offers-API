using FleetingOffers.Http;
using FleetingOffers.Module.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller.Dev;

[Route("dev/auth")]
[ApiController]
public class DevAuthController : DevControllerBase
{
    private readonly DevAuthService _service;

    public DevAuthController(DevAuthService service)
    {
        _service = service;
    }
    [HttpPost("set-password")]
    public IActionResult SetPassword(SetPasswordAdminPayload body)
    {
        try
        {
            _service.SetPassword(body.Email, body.Password, body.Otp);
            return AppHttpResponse.Ok("Password Set, Please login with your new password");
        }
        catch (Exception e)
        {
            return AppHttpResponse.BadRequest($"Failed to set password: {e.Message}");
        }
    }
}