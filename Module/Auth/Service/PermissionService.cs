using FleetingOffers.Http;

namespace FleetingOffers.Module.Auth;

class PermissionService
{
    public static bool HasPermission<T>(HttpPayloadDto payload, string module, string accessLevel)
    {
        if (typeof(T).GetProperty("CreatedBy") == null) throw new Exception("Cannot identify resource owner, permission denied");
        string basePath = AppContext.BaseDirectory;
        string filePath = Path.Combine(basePath, "permissions.json");

        string json = File.ReadAllText(filePath);
        return false;
    }
}