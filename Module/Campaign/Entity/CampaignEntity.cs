namespace FleetingOffers.Module.Campaign;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Module.File;

public class CampaignEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    public string? Description { get; set; }

    // Foreign Keys for Cover and Thumbnail Images
    public string? CoverImageId { get; set; }
    public FileEntity? CoverImage { get; set; }

    public string? ThumbnailImageId { get; set; }
    public FileEntity? ThumbnailImage { get; set; }

    // One-to-Many Relationship with CampaignAdvertises
    public ICollection<CampaignAdvertiseEntity> CampaignAdvertises { get; set; } = new List<CampaignAdvertiseEntity>();
}

public class CampaignAdvertiseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    [ForeignKey("Campaigns")]
    public string CampaignId { get; set; }  // Foreign Key to CampaignEntity

    [Required]
    [ForeignKey("Advertises")]
    public string AdvertiseId { get; set; }  // Foreign Key to AdvertiseEntity
    public AdvertiseEntity Advertise { get; set; }
}
