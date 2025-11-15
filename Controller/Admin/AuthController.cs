using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Auth;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/auth")]
[ApiController]
public class AuthController : AdminControllerBase
{
    private readonly AuthService _service;
    public AuthController(AuthService service)
    {
        _service = service;
    }
    [HttpPost("get-otp")]
    public IActionResult GetOtp(GetOtpAdminPayload body)
    {
        try
        {
            var email = body.Email;
            var Otp = _service.GetOtp(email);
            return AppHttpResponse.Ok("OTP Successfully sent to your email");
        }
        catch (Exception e)
        {
            return AppHttpResponse.BadRequest(e.Message);
        }
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

    [HttpPost("login")]
    public IActionResult Login(LoginPayloadAdminDto body)
    {
        try
        {
            var res = _service.Login(body.Email, body.Password, body.Device);
            return AppHttpResponse.Ok(res, "Login Successful");
        }
        catch (Exception e)
        {
            return AppHttpResponse.BadRequest(e.Message);
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.AUTH,
            "_",
            async () =>
            {
                try
                {
                    var AuthorizationResponse = HttpContext.Items["AuthorizationPayload"] as TokenValidationResponse ?? throw new Exception("System Error");
                    _service.Logout(AuthorizationResponse.UserId!, AuthorizationResponse.Token!, AuthorizationResponse.Device);
                    return AppHttpResponse.Ok("Logout Successful");
                }
                catch (Exception e)
                {
                    return AppHttpResponse.BadRequest(e.Message);
                }
            },
            true
        );
    }

        [HttpGet("validate-token")]
    public IActionResult ValidateToken()
    {
        try
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return AppHttpResponse.BadRequest("MISSING_TOKEN: Authorization header with Bearer token is required");
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                return AppHttpResponse.BadRequest("INVALID_TOKEN: Token cannot be empty");
            }

            var res = _service.ValidateTokenWithPermissions(token);
            if (res == null || !res.IsValid) 
            {
                return AppHttpResponse.Unauthorized();
            }
            
            return AppHttpResponse.Ok(res, "Token is valid");
        }
        catch (Exception e)
        {
            return AppHttpResponse.BadRequest(e.Message);
        }
    }   

}