using AutoMapper;
using AutoMapper.QueryableExtensions;
using FleetingOffers.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static FleetingOffers.AppDbContext;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    public AdvertiseRepository(AppDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }
    public async Task<AdvertiseProjection_AllDto?> GetOwnAdvertiseWithAllAsync(string userId, string id)
    {
        // Not implemented
        var advertiseDto = await _dbContext.Advertises
            .AsQueryable()
            .Where(a => a.Id == id && (a.Owners.Any(o => o.UserId == userId) || a.CreatedById == userId))
            .ProjectTo<AdvertiseProjection_AllDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return advertiseDto;
    }
    public async Task<AdvertiseDto> CreateAdvertiseAsync(
        string createdBy,
        AdvertiseDto dto,
        List<AdvertiseOwnerDto> owners
    )
    {
        // Not implemented
        AdvertiseEntity advertiseEntity = _mapper.Map<AdvertiseEntity>(dto);
        List<AdvertiseOwnerEntity> ownersEntity = new();

        advertiseEntity.CreatedById = createdBy;
        _dbContext.Add(advertiseEntity);

        await _dbContext.SaveChangesWithUploadsAsync(new[]
        {
            new SaveChangesWithUploadsAsyncPropsDto("CoverImageId", new[] { "image/jpeg", "image/png" }),
            new SaveChangesWithUploadsAsyncPropsDto("ThumbnailImageId", new[] { "image/jpeg", "image/png", "image/webp" }),
        });

        foreach (var owner in owners)
        {
            owner.AdvertiseId = advertiseEntity.Id;
            ownersEntity.Add(_mapper.Map<AdvertiseOwnerEntity>(owner));
        }

        _dbContext.AddRange(ownersEntity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<AdvertiseDto>(advertiseEntity);
    }
}