using System.Runtime.CompilerServices;
using FleetingOffers.Http;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/advertise")]
public class AdvertiseController : ControllerBase {
    public readonly AdvertiseAdminControllerService _service;
    public AdvertiseController (AdvertiseAdminControllerService service) {
        _service = service;
    }
    [HttpPost("/create")]
    public IActionResult CreateAdvertise([FromBody] CreateAdvertisePayloadDto body) {
        try {
            var payload = HttpHelper.GetAuthorizationPayload(HttpContext);
            if (payload == null || payload.UserId == null) return AppHttpResponse.BadRequest("Failed to Process Authorization");
            var owners = new List<AdvertiseOwnerPayloadDto> { new AdvertiseOwnerPayloadDto { UserId = payload.UserId, OwnershipType = ADVERTISE_OWNERSHIP.OWNER } };
            var advertise = _service.CreateAdvertise(owners, body);
            return AppHttpResponse.Ok(advertise);
        } catch (Exception e) {
            return AppHttpResponse.BadRequest(e.Message);
        }
    }

    [HttpPost("/create/by-admin")]
    public IActionResult CreateAdvertiseByAdmin([FromBody] CreateAdvertiseByAdminPayloadDto body) {
        try {
            var advertise = _service.CreateAdvertise(body.Owners, body.Advertise);
            return AppHttpResponse.Ok(advertise);
        } catch (Exception e) {
            return AppHttpResponse.BadRequest(e.Message);
        }
    }
}