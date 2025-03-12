namespace FleetingOffers.Module.Location;

public class LocationDto
{
    public string Id { get; set; }  // Auto-generated ID
    public double Lat { get; set; }  // Latitude (Required)
    public double Long { get; set; }  // Longitude (Required)
    public string? AddressText { get; set; }  // Full address (Optional)
    public string District { get; set; }  // Administrative district
    public string Upazila { get; set; }  // Sub-district or Upazila
    public string? Union { get; set; }  // Union (Optional)
    public string? Village { get; set; }  // Village (Optional)
    public string? Area { get; set; }  // Area (Optional)
    public string? PostalCode { get; set; }  // Postal code (Optional)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Timestamp of creation
}
