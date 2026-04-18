using System.ComponentModel.DataAnnotations;

namespace FleetingOffers.Module.Product;

public class CreateProductCategoryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // Foreign Keys for Images
    public string? ImageId { get; set; }
}

public class UpdateProductCategoryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // Foreign Keys for Images
    public string? ImageId { get; set; }
}
