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

public class HttpAuthenticate : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var bearer = context.HttpContext.Request.Headers.Authorization;
        var token = bearer.ToString().Replace("Bearer ", "").Trim();
        var serviceProvider = context.HttpContext.RequestServices;
        var scope = serviceProvider.CreateScope();

        var authService = scope.ServiceProvider.GetRequiredService<AuthService>();

        var ValidationResponse = authService.ValidateToken(token);

        // Storing the retrerived user in the context
        context.HttpContext.Items["AuthenticationResponse"] = ValidationResponse;

    }
}
