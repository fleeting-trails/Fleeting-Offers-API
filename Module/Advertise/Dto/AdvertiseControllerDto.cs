using FleetingOffers.Module.Advertise;

public class CreateAdvertisePayloadDto
{
    public string Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? ExpirationDate { get; set; }

    // Foreign Keys for Images
    public string? CoverImageId { get; set; }

    public string? ThumbnailImageId { get; set; }

    // Foreign Keys for Category & Industry
    public string? CategoryId { get; set; }

    public string? SubCategoryId { get; set; }
}
public class CreateAdvertiseByAdminPayloadDto
{
    public List<AdvertiseOwnerPayloadDto> Owners { get; set; }
    public CreateAdvertisePayloadDto Advertise { get; set;}
}

public class AdvertiseOwnerPayloadDto
{
    public string UserId { get; set; }
    public ADVERTISE_OWNERSHIP OwnershipType { get; set; }
}