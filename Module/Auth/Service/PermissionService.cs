using System.Text.Json;
using FleetingOffers.Common.Dto;
using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.User;
using Org.BouncyCastle.Asn1.Misc;
using YamlDotNet.Core.Tokens;

namespace FleetingOffers.Module.Auth;

class PermissionService
{
    public static void CheckPermission(HttpPayloadDto payload, APP_MODULE module, string action, bool allowAllLoggedInUsers = false)
    {
        if (!allowAllLoggedInUsers)
        {
            string basePath = AppContext.BaseDirectory;
            string filePath = Path.Combine(basePath, "permissions.json");
            Console.WriteLine($"Permission file path: {filePath}");

            string json = File.ReadAllText(filePath);
            var permissions = JsonSerializer.Deserialize<PermissionDto>(json);

            if (permissions == null) throw new Exception("Unauthorized: Failed to Check Your Authorization.");

            if (permissions.TryGetValue(payload.Role, out var rolePermissions) &&
                rolePermissions.TryGetValue(module, out var modulePermissions) &&
                modulePermissions.TryGetValue(action, out var scope))
            {
                if (scope == false)
                {
                    throw new Exception("Unauthorized: Access Denied");
                }
            }
            else
            {
                throw new Exception("Unauthorized: Access Denied");
            }
        }
    }
}