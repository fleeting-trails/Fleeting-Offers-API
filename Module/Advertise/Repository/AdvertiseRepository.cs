using AutoMapper;
using AutoMapper.QueryableExtensions;
using FleetingOffers.Attributes;
using FleetingOffers.Http;
using Microsoft.EntityFrameworkCore;
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
    public async Task<AdvertiseProjection_AllDto?> GetAdvertiseWithAllAsync(string id)
    {
        // Not imp.emented
        var advertiseDto = await _dbContext.Advertises
            .AsQueryable()
            .Where(a => a.Id == id)
            .ProjectTo<AdvertiseProjection_AllDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return advertiseDto;
    }
    public async Task<PaginatedResult<AdvertiseDto>> GetOwnAdvertisesPaginatedAsync(string userId, int page, int pageSize)
    {
        // Not implemented
        var query = _dbContext.Advertises
            .AsQueryable()
            .Where(a => a.Owners.Any(o => o.UserId == userId) || a.CreatedById == userId)
            .OrderByDescending(a => a.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var totalItems = await query.CountAsync();
        var items = await query.ProjectTo<AdvertiseDto>(_mapper.ConfigurationProvider).ToListAsync();

        return new PaginatedResult<AdvertiseDto>
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }
    public async Task<PaginatedResult<AdvertiseDto>> GetAdvertisesPaginatedAsync(int page, int pageSize)
    {
        // Not implemented
        var query = _dbContext.Advertises
            .AsQueryable()
            .OrderByDescending(a => a.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var totalItems = await query.CountAsync();
        var items = await query.ProjectTo<AdvertiseDto>(_mapper.ConfigurationProvider).ToListAsync();

        return new PaginatedResult<AdvertiseDto>
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
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

    public async Task<AdvertiseDto> UpdateAdvertiseAsync(
        string userId,
        AdvertiseDto dto
    )
    {
    var entity = await _dbContext.Advertises
        .Where(a => a.Id == dto.Id && (a.Owners.Any(o => o.UserId == userId) || a.CreatedById == userId))
        .FirstOrDefaultAsync();

    if (entity == null)
        throw new KeyNotFoundException("Advertise not found");

    // Update simple fields
    entity.Title = dto.Title;
    entity.Subtitle = dto.Subtitle;
    entity.Description = dto.Description;
    entity.StartDate = dto.StartDate;
    entity.ExpirationDate = dto.ExpirationDate;
    entity.UpdatedAt = DateTime.UtcNow;

    _dbContext.Advertises.Update(entity);
    await _dbContext.SaveChangesAsync();

    return _mapper.Map<AdvertiseDto>(entity);
}
}