using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/advertise")]
[ApiController]
public class AdvertiseController : AdminControllerBase
{
    private readonly AdvertiseControllerService _service;
    public AdvertiseController(AdvertiseControllerService advertiseService)
    {
        _service = advertiseService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAdvertise([FromBody] CreateAdvertiseDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "CREATE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.CreateAdvertiseAsync(authPayload.UserId, dto);
                    return AppHttpResponse.Ok("Ok");
                }
                catch (Exception ex)
                {
                    return AppHttpResponse.BadRequest(ex.Message);
                }
            }
        );

    }
    [HttpPost("create-by-admin")]
    public async Task<IActionResult> CreateAdvertiseByAdmin([FromBody] CreateAdvertiseAdminDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "CREATE_BY_ADMIN",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.CreateAdvertiseByAdminAsync(authPayload.UserId, dto);
                    return AppHttpResponse.Ok("Ok");
                }
                catch (Exception ex)
                {
                    return AppHttpResponse.BadRequest(ex.Message);
                }
            }
        );

    }
}