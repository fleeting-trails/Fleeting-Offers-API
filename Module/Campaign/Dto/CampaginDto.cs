namespace FleetingOffers.Module.Campaign;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Module.File;

public class CampaignDto
{
    public string Id { get; set; }  // Auto-generated ID

    public string Title { get; set; }

    public string? Description { get; set; }

    public string? CoverImageId { get; set; }

    public string? ThumbnailImageId { get; set; }
}
public class CampaignAdvertiseDto
{
    public string Id { get; set; }  // Auto-generated ID

    public string CampaignId { get; set; }  // Foreign Key to CampaignEntity

    public string AdvertiseId { get; set; }  // Foreign Key to AdvertiseEntity
}
