using System.ComponentModel.DataAnnotations;

namespace FleetingOffers.Module.Advertise;

public class CreateAdvertiseDto
{
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }

    [MaxLength(255)]
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


public class CreateAdvertiseAdminDto
{
    public CreateAdvertiseDto Advertise { get; set; }
    public List<_CreateAdvertiseOwnerInCreateAdvertiseDto> Owners { get; set; }
}

#region DependentDtos
public class _CreateAdvertiseOwnerInCreateAdvertiseDto
{
    public string UserId { get; set; }
    public ADVERTISE_OWNERSHIP OwnershipType { get; set; }  // Enum
    
}
#endregion