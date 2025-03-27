using FleetingOffers.Common.Enum;
using FleetingOffers.Modifier;
using FleetingOffers.Module.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Http;

[HttpAuthenticateDev]
public class DevControllerBase : ControllerBase {
}