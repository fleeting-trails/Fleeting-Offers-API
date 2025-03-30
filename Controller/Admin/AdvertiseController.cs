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

    /// <summary>
    /// Get an advertise.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization
    /// </remarks>
    /// <response code="200">AD Details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPost("get")]
    public async Task<IActionResult> GetAdvertise(string advertiseId)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "READ_OWN",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetAdvertiseAsync(authPayload.UserId, advertiseId);
                    return AppHttpResponse.Ok(res, "Ok");
                }
                catch (Exception ex)
                {
                    return AppHttpResponse.BadRequest(ex.Message);
                }
            }
        );

    }

    /// <summary>
    /// Creates an advertise entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization
    /// </remarks>
    /// <response code="200">Ad created</response>
    /// <response code="401">Unauthorized: Access Denied</response>
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
    /// <summary>
    /// Creates an advertise for organization by admin.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">Ad created</response>
    /// <response code="401">Unauthorized: Access Denied</response>
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