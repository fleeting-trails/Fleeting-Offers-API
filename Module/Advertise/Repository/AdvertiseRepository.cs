using AutoMapper;
using FleetingOffers.Attributes;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseRepository
{
    public readonly AppDbContext _dbContext;
    public readonly IMapper _mapper;
    public AdvertiseRepository(
        AppDbContext dbContext,
        IMapper mapper
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public AdvertiseDto CreateAdvertise(List<AdvertiseOwnerPayloadDto> owners, CreateAdvertisePayloadDto payloadDto, string createdBy)
    {
        var advertise = new AdvertiseEntity
        {
            Title = payloadDto.Title,
            Subtitle = payloadDto.Subtitle,
            Description = payloadDto.Description,
            StartDate = payloadDto.StartDate,
            ExpirationDate = payloadDto.ExpirationDate,
            CoverImageId = payloadDto.CoverImageId,
            ThumbnailImageId = payloadDto.ThumbnailImageId,
            CategoryId = payloadDto.CategoryId,
            SubCategoryId = payloadDto.SubCategoryId,
            CreatedBy = createdBy
        };
        _dbContext.Advertises.Add(advertise);
        _dbContext.SaveChangesWithUploads(["CoverImageId", "ThumbnailImageId"]);
        foreach (var owner in owners)
        {
            var advertiseOwner = new AdvertiseOwnerEntity()
            {
                AdvertiseId = advertise.Id,
                UserId = owner.UserId,
                OwnershipType = owner.OwnershipType
            };
            _dbContext.AdvertiseOwners.Add(advertiseOwner);
        }
        _dbContext.SaveChanges();
        return _mapper.Map<AdvertiseDto>(advertise);
    }
}