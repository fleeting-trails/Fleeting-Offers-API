using FleetingOffers.Common.Enum;
using FleetingOffers.Modifier;
using FleetingOffers.Module.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Http;

[HttpAuthenticate]
public class AdminControllerBase : ControllerBase {
    public static async Task<IActionResult> WithPermission(
        HttpContext httpContext,
        APP_MODULE module,
        string action,
        Func<Task<IActionResult>> actionToRun,
        bool allowAllLoggedInUsers = false
    )
    {
        try
        {
            var payload = HttpHelper.GetAuthorizationPayload(httpContext);
            PermissionService.CheckPermission(payload, module, action, allowAllLoggedInUsers);

            return await actionToRun();
        }
        catch (Exception ex)
        {
            return new UnauthorizedObjectResult(ex.Message);
        }
    }
}