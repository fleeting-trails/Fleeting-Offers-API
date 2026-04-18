using System.ComponentModel.DataAnnotations;

namespace FleetingOffers.Module.Advertise;

public class CreateAdvertiseIndustryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // Foreign Keys for Images
    public string? ImageId { get; set; }
}

public class UpdateAdvertiseIndustryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // Foreign Keys for Images
    public string? ImageId { get; set; }
}