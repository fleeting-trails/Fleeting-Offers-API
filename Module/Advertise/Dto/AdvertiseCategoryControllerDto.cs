using System.ComponentModel.DataAnnotations;

namespace FleetingOffers.Module.Advertise;

public class CreateAdvertiseCategoryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // Foreign Keys for Images
    public string? ImageId { get; set; }
}

public class UpdateAdvertiseCategoryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // Foreign Keys for Images
    public string? ImageId { get; set; }
}