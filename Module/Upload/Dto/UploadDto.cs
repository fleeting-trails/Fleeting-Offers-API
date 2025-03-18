namespace FleetingOffers.Module.Upload;

public class UploadDto
{
    public string Id { get; set; }  // Auto-generated ID

    public string Name { get; set; }  // Unique file name (e.g., UUID-based)

    public string? OriginalName { get; set; }  // Original file name from the user (optional)

    public string URL { get; set; }  // Path or URL to the file location

    public UPLOAD_STORAGE_TYPE Storage { get; set; } = UPLOAD_STORAGE_TYPE.LOCAL;  // Default is Local storage

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default creation timestamp

    public DateTime? UpdatedAt { get; set; }  // Timestamp for last update (optional)
}