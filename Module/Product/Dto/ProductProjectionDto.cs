namespace FleetingOffers.Module.Product;

public class ProductProjection_AllDto : ProductDto
{
    public ICollection<ProductAdditionalImageEntity> AdditionalImages { get; set; } = new List<ProductAdditionalImageEntity>();
    public ICollection<ProductTagEntity> Tags { get; set; } = new List<ProductTagEntity>();
    public ICollection<ProductOwnerEntity> Owners { get; set; } = new List<ProductOwnerEntity>();
}

public class ProductCategoryProjection_DetailDto : ProductCategoryDto
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? ImageId { get; set; }
    public DateTime CreatedAt { get; set; }
}
