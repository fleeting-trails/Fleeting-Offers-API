namespace FleetingOffers.Module.Product;

public class ProductProjection_AllDto : ProductDto
{
}

public class ProductCategoryProjection_DetailDto : ProductCategoryDto
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? ImageId { get; set; }
    public DateTime CreatedAt { get; set; }
}
