using AutoMapper;
using FleetingOffers.Attributes;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseRepository {
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    public AdvertiseRepository(AppDbContext context, IMapper mapper) {
        _dbContext = context;
        _mapper = mapper;
    }
    public async Task<AdvertiseDto> CreateAdvertiseAsync(
        string createdBy, 
        AdvertiseDto dto, 
        List<AdvertiseOwnerDto> owners
    ) {
        // Not implemented
        AdvertiseEntity advertiseEntity = _mapper.Map<AdvertiseEntity>(dto);
        List<AdvertiseOwnerEntity> ownersEntity = new();

        advertiseEntity.CreatedById = createdBy;
        _dbContext.Add(advertiseEntity);

        foreach (var owner in owners) {
            owner.AdvertiseId = advertiseEntity.Id;
            ownersEntity.Add(_mapper.Map<AdvertiseOwnerEntity>(owner));
        }
        
        _dbContext.AddRange(ownersEntity);
        await _dbContext.SaveChangesWithUploadsAsync(["CoverImageId", "ThumbnailImageId"]);

        return _mapper.Map<AdvertiseDto>(advertiseEntity);
    }
}