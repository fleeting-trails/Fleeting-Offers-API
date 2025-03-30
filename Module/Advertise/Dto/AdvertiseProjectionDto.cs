namespace FleetingOffers.Module.Advertise;

public class AdvertiseProjection_AllDto : AdvertiseDto {
    public ICollection<AdvertiseLocationEntity> Locations { get; set; } = new List<AdvertiseLocationEntity>();
    public ICollection<AdvertiseRelatedAdvertiseEntity> RelatedAdvertises { get; set; } = new List<AdvertiseRelatedAdvertiseEntity>();
    public ICollection<AdvertiseAdditionalImageEntity> AdditionalImages { get; set; } = new List<AdvertiseAdditionalImageEntity>();
    public ICollection<AdvertiseTagEntity> Tags { get; set; } = new List<AdvertiseTagEntity>();
    public ICollection<AdvertiseOwnerEntity> Owners { get; set; } = new List<AdvertiseOwnerEntity>();
    public AdvertiseAnalyticsEntity? Analytics { get; set; }
}