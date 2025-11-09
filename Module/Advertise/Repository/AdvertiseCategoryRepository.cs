using AutoMapper;
using AutoMapper.QueryableExtensions;
using FleetingOffers.Attributes;
using FleetingOffers.Http;
using Microsoft.EntityFrameworkCore;
using static FleetingOffers.AppDbContext;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseCategoryRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    public AdvertiseCategoryRepository(AppDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    // Get Category Details
    public async Task<AdvertiseCategoryDto?> GetCategoryByIdAsync(string id)
    {
        var categoryDetailsDto = await _dbContext.AdvertiseCategories
            .AsQueryable()
            .Where(a => a.Id == id)
            .ProjectTo<AdvertiseCategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return categoryDetailsDto;
    }

    // category list paginated
    public async Task<PaginatedResult<AdvertiseCategoryDto>> GetCategoriesPaginatedAsync(int page, int pageSize)
    {
        var query = _dbContext.AdvertiseCategories
            .AsQueryable()
            .OrderByDescending(a => a.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var totalItems = await query.CountAsync();
        var items = await query.ProjectTo<AdvertiseCategoryDto>(_mapper.ConfigurationProvider).ToListAsync();

        return new PaginatedResult<AdvertiseCategoryDto>
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }
    
    // Create Category
    public async Task<AdvertiseCategoryDto> CreateAdvertiseCategoryAsync(
        AdvertiseCategoryDto dto
    )
    {
        AdvertiseCategoryEntity advertiseCategoryEntity = _mapper.Map<AdvertiseCategoryEntity>(dto);
        
        _dbContext.Add(advertiseCategoryEntity);

        await _dbContext.SaveChangesWithUploadsAsync(new[]
        {
            new SaveChangesWithUploadsAsyncPropsDto("ImageId", new[] { "image/jpeg", "image/png", "image/webp" }),
        });

        return _mapper.Map<AdvertiseCategoryDto>(advertiseCategoryEntity);
    }

    // Update Category
    public async Task<AdvertiseCategoryDto> UpdateAdvertiseCategoryAsync(
        AdvertiseCategoryDto dto
    )
    {
        var entity = await _dbContext.AdvertiseCategories
            .Where(a => a.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
            throw new KeyNotFoundException("Advertise category not found");

        // Update simple fields
        entity.Name = dto.Name;
        entity.Slug = dto.Slug;
        entity.ImageId = dto.ImageId;

        _dbContext.AdvertiseCategories.Update(entity);
        await _dbContext.SaveChangesWithUploadsAsync(new[]
        {
            new SaveChangesWithUploadsAsyncPropsDto("ImageId", new[] { "image/jpeg", "image/png", "image/webp" }),
        });

        return _mapper.Map<AdvertiseCategoryDto>(entity);
    }

    // Delete Category
    public async Task DeleteAdvertiseCategoryAsync(string id)
    {
        var entity = await _dbContext.AdvertiseCategories
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();

        if (entity == null)
            throw new KeyNotFoundException("Advertise category not found");

        _dbContext.AdvertiseCategories.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}