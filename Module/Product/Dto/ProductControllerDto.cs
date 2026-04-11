using System.ComponentModel.DataAnnotations;

namespace FleetingOffers.Module.Product;

public class CreateProductDto
{
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string? CoverImageId { get; set; }
    public string? ThumbnailImageId { get; set; }

    public string? CategoryId { get; set; }
    public string? SubCategoryId { get; set; }
    public string? DealId { get; set; }

    public List<string>? Tags { get; set; }
}

public class UpdateProductDetailsDto
{
    [Required]
    public string Id { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    // Foreign Keys for Images
    public string? CoverImageId { get; set; }
    public string? ThumbnailImageId { get; set; }

    // Foreign Keys for Category & Industry
    public string? CategoryId { get; set; }
    public string? SubCategoryId { get; set; }
    public string? DealId { get; set; }
}

public class CreateProductAdminDto
{
    public CreateProductDto Product { get; set; }
    public List<_CreateProductOwnerInCreateProductDto> Owners { get; set; }
}

#region DependentDtos
public class _CreateProductOwnerInCreateProductDto
{
    public string UserId { get; set; }
    public PRODUCT_OWNERSHIP OwnershipType { get; set; }
}
#endregion
