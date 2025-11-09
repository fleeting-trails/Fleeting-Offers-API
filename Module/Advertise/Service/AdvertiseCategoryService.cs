using AutoMapper;
using FleetingOffers.Attributes;
using FleetingOffers.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseCategoryService
{
    private readonly AdvertiseCategoryRepository _repository;
    private readonly AdvertiseValidator _validator;
    private readonly IMapper _mapper;

    public AdvertiseCategoryService(
        AdvertiseCategoryRepository repository,
        AdvertiseValidator validator,
        IMapper mapper
    )
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;

    }

    // Get Category Details
    public async Task<AdvertiseCategoryDto> GetCategoryByIdAsync(string id)
    {
        // Implementation
        var categoryDetails = await _repository.GetCategoryByIdAsync(id);
        if (categoryDetails == null)
        {
            throw new Exception("CATEGORY_404: No Category found with this ID");
        }
        return categoryDetails;
    }

    // Category List Paginated
    public async Task<PaginatedResult<AdvertiseCategoryDto>> GetCategoriesPaginatedAsync(int page, int pageSize)
    {
        // Implementation
        var result = await _repository.GetCategoriesPaginatedAsync(page, pageSize);
        if (result == null)
        {
            throw new Exception("FAILED: Failed to fetch list of categories");
        }
        return result;
    }

    // Create Advertise Category
    public async Task CreateAdvertiseCategoryAsync(CreateAdvertiseCategoryDto dto)
    {
        _validator.ValidateCreateAdvertiseCategory(dto);
        var advertiseCategoryDto = _mapper.Map<AdvertiseCategoryDto>(dto);
        await _repository.CreateAdvertiseCategoryAsync(advertiseCategoryDto);
    }

    // Update Advertise Category
    public async Task UpdateAdvertiseCategoryAsync(string id, UpdateAdvertiseCategoryDto dto)
    {
        _validator.ValidateUpdateAdvertiseCategory(dto);
        var advertiseCategoryDto = _mapper.Map<AdvertiseCategoryDto>(dto);
        advertiseCategoryDto.Id = id; // Ensure the ID is set for the update
        await _repository.UpdateAdvertiseCategoryAsync(advertiseCategoryDto);
    }

    // Delete Advertise Category
    public async Task DeleteAdvertiseCategoryAsync(string id)
    {
        await _repository.DeleteAdvertiseCategoryAsync(id);
    }
}