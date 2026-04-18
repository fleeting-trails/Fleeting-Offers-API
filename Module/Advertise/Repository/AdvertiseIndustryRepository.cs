using AutoMapper;
using AutoMapper.QueryableExtensions;
using FleetingOffers.Attributes;
using FleetingOffers.Http;
using Microsoft.EntityFrameworkCore;
using static FleetingOffers.AppDbContext;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseIndustryRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public AdvertiseIndustryRepository(AppDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    // Get Industry Details
    public async Task<AdvertiseIndustryDto?> GetIndustryByIdAsync(string id)
    {
        var industryDetailsDto = await _dbContext.AdvertiseIndustries
            .AsQueryable()
            .Where(a => a.Id == id)
            .ProjectTo<AdvertiseIndustryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return industryDetailsDto;
    }

    // Industry list paginated
    public async Task<PaginatedResult<AdvertiseIndustryDto>> GetIndustriesPaginatedAsync(int page, int pageSize)
    {
        var query = _dbContext.AdvertiseIndustries
            .AsQueryable()
            .OrderByDescending(a => a.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var totalItems = await query.CountAsync();
        var items = await query.ProjectTo<AdvertiseIndustryDto>(_mapper.ConfigurationProvider).ToListAsync();

        return new PaginatedResult<AdvertiseIndustryDto>
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }
    
    // Create Industry
    public async Task<AdvertiseIndustryDto> CreateAdvertiseIndustryAsync(
        AdvertiseIndustryDto dto
    )
    {
        AdvertiseIndustryEntity advertiseIndustryEntity = _mapper.Map<AdvertiseIndustryEntity>(dto);
        
        _dbContext.Add(advertiseIndustryEntity);

        await _dbContext.SaveChangesWithUploadsAsync(new[]
        {
            new SaveChangesWithUploadsAsyncPropsDto("ImageId", new[] { "image/jpeg", "image/png", "image/webp" }),
        });

        return _mapper.Map<AdvertiseIndustryDto>(advertiseIndustryEntity);
    }

    // Update Industry
    public async Task<AdvertiseIndustryDto> UpdateAdvertiseIndustryAsync(
        AdvertiseIndustryDto dto
    )
    {
        var entity = await _dbContext.AdvertiseIndustries
            .Where(a => a.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
            throw new KeyNotFoundException("Advertise industry not found");

        // Update simple fields
        entity.Name = dto.Name;
        entity.Slug = dto.Slug;
        entity.ImageId = dto.ImageId;

        _dbContext.AdvertiseIndustries.Update(entity);
        await _dbContext.SaveChangesWithUploadsAsync(new[]
        {
            new SaveChangesWithUploadsAsyncPropsDto("ImageId", new[] { "image/jpeg", "image/png", "image/webp" }),
        });

        return _mapper.Map<AdvertiseIndustryDto>(entity);
    }

    // Delete Industry
    public async Task DeleteAdvertiseIndustryAsync(string id)
    {
        var entity = await _dbContext.AdvertiseIndustries
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();

        if (entity == null)
            throw new KeyNotFoundException("Advertise industry not found");

        _dbContext.AdvertiseIndustries.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}