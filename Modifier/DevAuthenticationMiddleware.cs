using FleetingOffers.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FleetingOffers.Modifier;

public class HttpAuthenticateDev : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var bearer = context.HttpContext.Request.Headers.Authorization;
        var token = bearer.ToString().Replace("Bearer ", "").Trim();

        if (token != AuthSettings.DeveloperSecret)
        {
            context.Result = new ContentResult
            {
                StatusCode = 401, // Set the desired status code
                Content = "Access denied due to invalid token.",
                ContentType = "application/json"
            };
        }
    }
}