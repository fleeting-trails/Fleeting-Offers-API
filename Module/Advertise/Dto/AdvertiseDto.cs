namespace FleetingOffers.Module.Advertise;

public class AdvertiseDto
{
    public string Id { get; set; }  // Auto-generated ID
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


public class AdvertiseOwnerDto
{
    public string Id { get; set; }

    public string AdvertiseId { get; set; }
    public string UserId { get; set; }
}

public class AdvertiseLocationDto

{
    public string Id { get; set; }
    public string AdvertiseId { get; set; }
}


public class AdvertiseDealTypeDto
{
    public string Id { get; set; }

    public string Name { get; set; }
}

public class AdvertiseRelatedAdvertiseDto
{
    public string Id { get; set; }

    public string AdvertiseId { get; set; }

    public string RelatedAdvertiseId { get; set; }
}

public class AdvertiseAdditionalImageDto
{
    public string Id { get; set; }

    public string AdvertiseId { get; set; }


    public string ImageId { get; set; }
}

public class AdvertiseCategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? ImageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class AdvertiseIndustryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? ImageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class AdvertiseTagDto
{
    public string Id { get; set; }
    public string Tag { get; set; }
    public string AdvertiseId { get; set; }
}
public class AdvertiseAnalyticsDto
{
    public string Id { get; set; }
    public string AdvertiseId { get; set; }
    public int Views { get; set; } = 0;
    public int Clicks { get; set; } = 0;
    public int Conversions { get; set; } = 0;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
