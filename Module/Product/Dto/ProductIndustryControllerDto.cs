using System.ComponentModel.DataAnnotations;

namespace FleetingOffers.Module.Product;

public class CreateProductIndustryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // Foreign Keys for Images
    public string? ImageId { get; set; }
}

public class UpdateProductIndustryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // Foreign Keys for Images
    public string? ImageId { get; set; }
}
