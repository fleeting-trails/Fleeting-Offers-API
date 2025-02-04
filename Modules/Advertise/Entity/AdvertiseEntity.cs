using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FleetingOffers.Modules.File;
using FleetingOffers.Modules.Location;
using FleetingOffers.Modules.User;
namespace FleetingOffers.Modules.Advertise;

public class AdvertiseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? ExpirationDate { get; set; }

    // Foreign Keys for Images
    public string? CoverImageId { get; set; }
    public FileEntity? CoverImage { get; set; }

    public string? ThumbnailImageId { get; set; }
    public FileEntity? ThumbnailImage { get; set; }

    // Foreign Keys for Category & Industry
    public string? CategoryId { get; set; }
    public AdvertiseCategoryEntity? Category { get; set; }

    public string? SubCategoryId { get; set; }
    public AdvertiseIndustryEntity? SubCategory { get; set; }

    // Foreign Key for Deal Type
    public string? DealTypeId { get; set; }
    public AdvertiseDealTypeEntity? DealType { get; set; }

    // Relationships
    public ICollection<AdvertiseLocationEntity> Locations { get; set; } = new List<AdvertiseLocationEntity>();
    public ICollection<AdvertiseRelatedAdvertiseEntity> RelatedAdvertises { get; set; } = new List<AdvertiseRelatedAdvertiseEntity>();
    public ICollection<AdvertiseAdditionalImageEntity> AdditionalImages { get; set; } = new List<AdvertiseAdditionalImageEntity>();
    public ICollection<AdvertiseTagEntity> Tags { get; set; } = new List<AdvertiseTagEntity>();
    public ICollection<AdvertiseOwnerEntity> Owners { get; set; } = new List<AdvertiseOwnerEntity>();

    public AdvertiseAnalyticsEntity? Analytics { get; set; }
}


public class AdvertiseOwnerEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string AdvertiseId { get; set; }
    public AdvertiseEntity Advertise { get; set; }

    [Required]
    public string UserId { get; set; }
    public UserEntity User { get; set; }

    [Required]
    public ADVERTISE_OWNERSHIP OwnershipType { get; set; }  // Enum
}

public class AdvertiseLocationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    [ForeignKey("Advertises")]
    public string AdvertiseId { get; set; }
    public LocationEntity Location { get; set; }
}


public class AdvertiseDealTypeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }
}

public class AdvertiseRelatedAdvertiseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [ForeignKey("Advertises")]
    public string AdvertiseId { get; set; }

    [Required]
    [ForeignKey("Advertises")]
    public string RelatedAdvertiseId { get; set; }
    public AdvertiseEntity RelatedAdvertise { get; set; }
}

public class AdvertiseAdditionalImageEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [ForeignKey("Advertises")]
    public string AdvertiseId { get; set; }

    [Required]
    [ForeignKey("Files")]

    public string ImageId { get; set; }
    public FileEntity Image { get; set; }
}

public class AdvertiseCategoryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Slug { get; set; }

    public string? ImageId { get; set; }
    public FileEntity? Image { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class AdvertiseIndustryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Slug { get; set; }

    public string? ImageId { get; set; }
    public FileEntity? Image { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class AdvertiseTagEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string Tag { get; set; }

    [Required]
    [ForeignKey("Advertises")]
    public string AdvertiseId { get; set; }
}

public class AdvertiseAnalyticsEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [ForeignKey("Advertises")]
    public string AdvertiseId { get; set; }
    public AdvertiseEntity Advertise { get; set; }

    public int Views { get; set; } = 0;
    public int Clicks { get; set; } = 0;
    public int Conversions { get; set; } = 0;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

