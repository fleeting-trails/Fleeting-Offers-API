using FleetingOffers.Module;

namespace FleetingOffers.Settings;

public class UploadSettings {
    public static UPLOAD_STORAGE_TYPE StorageType;
    public static string StoragePath;
    public static List<string> AllowedExtensions;

    public UploadSettings (WebApplicationBuilder builder) {
        StorageType = UPLOAD_STORAGE_TYPE.LOCAL;
        StoragePath = builder.Configuration["UploadSettings:StoragePath"];
        var _extensionsStr = builder.Configuration["UploadSettings:AllowedExtensions"];
        var _extension = _extensionsStr != null ? _extensionsStr.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(ext => ext.Trim()).ToList() : [".pdf", ".png", ".jpg", ".jpeg"];
        AllowedExtensions = _extension;
    }
}