using System;
using FleetingOffers.Settings;
using FleetingOffers.Module.Auth;
using FleetingOffers.Module.User;
using FleetingOffers.Provider;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FleetingOffers.Configurations;

namespace FleetingOffers.Modifier;

public class HttpAuthorize : Attribute, IAuthorizationFilter
{
    public static string _role;

    public HttpAuthorize(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var bearer = context.HttpContext.Request.Headers.Authorization;
        var token = bearer.ToString().Replace("Bearer ", "").Trim();
        var serviceProvider = context.HttpContext.RequestServices;
        var scope = serviceProvider.CreateScope();

        var authService = scope.ServiceProvider.GetRequiredService<AuthService>();

        var ValidationResponse = authService.ValidateToken(token);

        var AllowedRoles = AuthSettings.RoleMapping.Find(role => role.Item1 == _role);

        if (!ValidationResponse.IsValid || ValidationResponse.UserId == null || ValidationResponse.Role == null || ValidationResponse.Token == null) {
            context.Result = new ContentResult
            {

                StatusCode = 401, // Set the desired status code
                Content = "Access denied due to invalid token.",
                ContentType = "application/json"

            };

            return;
        }
        if (!Array.Exists(AllowedRoles.Item2, element => element == ValidationResponse.Role))
        {

            context.Result = new ContentResult
            {

                StatusCode = 401, // Set the desired status code
                Content = "Access denied due to insufficient roles.",
                ContentType = "application/json"

            };

            return;
        }

        // Storing the retrerived user in the context
        context.HttpContext.Items["AuthorizationPayload"] = ValidationResponse;

    }
}
