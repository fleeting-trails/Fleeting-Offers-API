using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/advertise")]
public class AdvertiseController : AdminControllerBase
{
    public readonly AdvertiseAdminControllerService _service;
    public AdvertiseController(AdvertiseAdminControllerService service)
    {
        _service = service;
    }
    [HttpPost("/create")]
    public async Task<IActionResult> CreateAdvertise([FromBody] CreateAdvertisePayloadDto body)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "CREATE",
            async () =>
            {
                try
                {
                    var payload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var owners = new List<AdvertiseOwnerPayloadDto> { new AdvertiseOwnerPayloadDto { UserId = payload.UserId, OwnershipType = ADVERTISE_OWNERSHIP.OWNER } };
                    var advertise = await _service.CreateAdvertiseAsync(owners, body, payload.UserId);
                    return AppHttpResponse.Ok(advertise);
                }
                catch (Exception e)
                {
                    return AppHttpResponse.BadRequest(e.Message);
                }
            }
        );
    }

    [HttpPost("/create/by-admin")]
    public async Task<IActionResult> CreateAdvertiseByAdmin([FromBody] CreateAdvertiseByAdminPayloadDto body)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "CREATE_IF_ALLOWED",
            async () =>
            {
                try
                {
                    var payload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var advertise = await _service.CreateAdvertiseAsync(body.Owners, body.Advertise, payload.UserId);
                    return AppHttpResponse.Ok(advertise);
                }
                catch (Exception e)
                {
                    return AppHttpResponse.BadRequest(e.Message);
                }
            }
        );
    }
}