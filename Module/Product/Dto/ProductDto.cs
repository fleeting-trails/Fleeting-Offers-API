namespace FleetingOffers.Module.Product;

public class ProductDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }

    // Foreign Keys for Images
    public string? CoverImageId { get; set; }
    public string? ThumbnailImageId { get; set; }

    // Foreign Keys for Category & Industry
    public string? CategoryId { get; set; }
    public string? SubCategoryId { get; set; }
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
    public string ImageId { get; set; }
}

public class ProductCategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? ImageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ProductIndustryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? ImageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ProductTagDto
{
    public string Id { get; set; }
    public string Tag { get; set; }
    public string ProductId { get; set; }
}
