using System;
using System.Text;
using FleetingOffers.Module.User;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FleetingOffers.Configurations;


public class AuthSettings
{
    public static List<(string, USER_ROLE[])> RoleMapping = new() {
        ("AdminOnly", [USER_ROLE.ADMIN]),
        ("SuperAdmin", [USER_ROLE.SUPER_ADMIN]),
        ("Admin", [USER_ROLE.ADMIN, USER_ROLE.SUPER_ADMIN]),
        ("User", [USER_ROLE.ADMIN, USER_ROLE.SUPER_ADMIN, USER_ROLE.ORGANIZATION])
    };
    public AuthSettings (WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JWTSettings.Issuer,
                    ValidAudience = JWTSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Secret!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Retrieve token from query string for WebSocket connections
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/chatHub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });


        builder.Services.AddAuthorization(options =>
        {
            foreach (var role in RoleMapping)
            {
                options.AddPolicy(role.Item1, policy =>
                {
                    string[] roleStrings = role.Item2.Select(role => role.ToString()).ToArray();
                    policy.RequireRole(roleStrings);
                });
            }
        });
    }
}
