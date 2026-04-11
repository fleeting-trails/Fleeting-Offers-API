namespace FleetingOffers.Module.Product;

using FleetingOffers.Module.Upload;

public class ProductDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public UploadDto? CoverImage { get; set; }
    public UploadDto? ThumbnailImage { get; set; }
    public string? CoverImageId { get; set; }
    public string? ThumbnailImageId { get; set; }

    public ProductCategoryDto? Category { get; set; }
    public ProductIndustryDto? SubCategory { get; set; }
    public ProductDealDto? Deal { get; set; }
    public string? DealId { get; set; }
    
    public List<ProductTagDto> Tags { get; set; } = new();
    public List<ProductAdditionalImageDto> AdditionalImages { get; set; } = new();
    
    public string CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class ProductOwnerDto
{
    public string Id { get; set; }
    public string ProductId { get; set; }
    public string UserId { get; set; }
    public PRODUCT_OWNERSHIP OwnershipType { get; set; }
}

public class ProductAdditionalImageDto
{
    public string Id { get; set; }
    public string ProductId { get; set; }
    public UploadDto? Image { get; set; }
}

public class ProductCategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public UploadDto? Image { get; set; }
    public string? ImageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ProductIndustryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public UploadDto? Image { get; set; }
    public string? ImageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ProductDealDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public UploadDto? Image { get; set; }
    public string? ImageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ProductTagDto
{
    public string Id { get; set; }
    public string Tag { get; set; }
    public string ProductId { get; set; }
}
