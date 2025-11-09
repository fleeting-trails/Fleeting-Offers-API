using AutoMapper;
using FleetingOffers.Attributes;
using FleetingOffers.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseIndustryService
{
    private readonly AdvertiseIndustryRepository _repository;
    private readonly AdvertiseValidator _validator;
    private readonly IMapper _mapper;

    public AdvertiseIndustryService(
        AdvertiseIndustryRepository repository,
        AdvertiseValidator validator,
        IMapper mapper
    )
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;

    }

    // Get Industry Details
    public async Task<AdvertiseIndustryDto> GetIndustryByIdAsync(string id)
    {
        // Implementation
        var industryDetails = await _repository.GetIndustryByIdAsync(id);
        if (industryDetails == null)
        {
            throw new Exception("INDUSTRY_404: No Industry found with this ID");
        }
        return industryDetails;
    }

    // Industry List Paginated
    public async Task<PaginatedResult<AdvertiseIndustryDto>> GetIndustriesPaginatedAsync(int page, int pageSize)
    {
        // Implementation
        var result = await _repository.GetIndustriesPaginatedAsync(page, pageSize);
        if (result == null)
        {
            throw new Exception("FAILED: Failed to fetch list of industries");
        }
        return result;
    }

    // Create Advertise Industry
    public async Task CreateAdvertiseIndustryAsync(CreateAdvertiseIndustryDto dto)
    {
        _validator.ValidateCreateAdvertiseIndustry(dto);
        var advertiseIndustryDto = _mapper.Map<AdvertiseIndustryDto>(dto);
        await _repository.CreateAdvertiseIndustryAsync(advertiseIndustryDto);
    }

    // Update Advertise Industry
    public async Task UpdateAdvertiseIndustryAsync(string id, UpdateAdvertiseIndustryDto dto)
    {
        _validator.ValidateUpdateAdvertiseIndustry(dto);
        var advertiseIndustryDto = _mapper.Map<AdvertiseIndustryDto>(dto);
        advertiseIndustryDto.Id = id; // Ensure the ID is set for the update
        await _repository.UpdateAdvertiseIndustryAsync(advertiseIndustryDto);
    }

    // Delete Advertise Industry
    public async Task DeleteAdvertiseIndustryAsync(string id)
    {
        await _repository.DeleteAdvertiseIndustryAsync(id);
    }
}