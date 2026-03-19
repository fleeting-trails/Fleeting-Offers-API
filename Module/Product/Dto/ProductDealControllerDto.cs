using System.ComponentModel.DataAnnotations;

namespace FleetingOffers.Module.Product;

public class CreateProductDealDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? ImageId { get; set; }
}

public class UpdateProductDealDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? ImageId { get; set; }
}