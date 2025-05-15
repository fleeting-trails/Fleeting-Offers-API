using FleetingOffers.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FleetingOffers.Modifier;

public class HttpAuthenticateDev : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var env = context.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
        var bearer = context.HttpContext.Request.Headers.Authorization;
        var token = bearer.ToString().Replace("Bearer ", "").Trim();

        var secret = AuthSettings.DeveloperSecret;
        if (token != secret)
        {
            context.Result = new ContentResult
            {
                StatusCode = 401, // Set the desired status code
                Content = "Access denied due to invalid token.",
                ContentType = "application/json"
            };
        }

        // Disable developer routes in production
        // Please place it only after the token validation.
        if (!env.IsDevelopment() && !env.IsStaging())
        {
            context.Result = new ContentResult
            {
                StatusCode = 403,
                Content = "Developer routes are disabled in this environment.",
                ContentType = "application/json"
            };
            return;
        }
    }
}