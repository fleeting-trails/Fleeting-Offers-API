using AutoMapper;
using FleetingOffers.Attributes;
using FleetingOffers.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseControllerService {
    private readonly AdvertiseRepository _repository;
    private readonly AdvertiseValidator _validator;
    private readonly IMapper _mapper;

    public AdvertiseControllerService(
        AdvertiseRepository repository, 
        AdvertiseValidator validator,
        IMapper mapper
    ) {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;

    }
    public async Task<AdvertiseDto> GetAdvertiseAsync (string id) {
        // Implementation
        var advertise = await _repository.GetAdvertiseWithAllAsync(id);
        if (advertise == null) {
            throw new Exception("ADVERTISE_404: No Advertise found with this ID");
        }
        return advertise;
    }
    public async Task<AdvertiseDto> GetOwnAdvertiseAsync (string userId, string id) {
        // Implementation
        var advertise = await _repository.GetOwnAdvertiseWithAllAsync(userId, id);
        if (advertise == null) {
            throw new Exception("ADVERTISE_404: No Advertise found with this ID");
        }
        return advertise;
    }
    public async Task<PaginatedResult<AdvertiseDto>> GetOwnAdvertisesPaginatedAsync(string userId, int page, int pageSize) {
        // Implementation
        var result = await _repository.GetOwnAdvertisesPaginatedAsync(userId, page, pageSize);
        if (result == null) {
            throw new Exception("FAILED: Failed to fetch list of advertises");
        }
        return result;
    }
    public async Task<PaginatedResult<AdvertiseDto>> GetAllAdvertisesPaginatedAsync(int page, int pageSize) {
        // Implementation
        var result = await _repository.GetAdvertisesPaginatedAsync(page, pageSize);
        if (result == null) {
            throw new Exception("FAILED: Failed to fetch list of advertises");
        }
        return result;
    }
    public async Task CreateAdvertiseAsync(string createdBy, CreateAdvertiseDto dto) {
        // Implementation
        _validator.ValidateCreateAdvertise(dto);
        AdvertiseOwnerDto owner = new AdvertiseOwnerDto {
            UserId = createdBy,
            OwnershipType = ADVERTISE_OWNERSHIP.OWNER
        };
        var advertiseDto = _mapper.Map<AdvertiseDto>(dto);
        await _repository.CreateAdvertiseAsync(createdBy, advertiseDto, [owner]);
    }
    public async Task CreateAdvertiseByAdminAsync(string createdBy, CreateAdvertiseAdminDto dto) {
        // Implementation
        _validator.ValidateCreateAdvertiseAdmin(dto);
        var advertiseDto = _mapper.Map<AdvertiseDto>(dto.Advertise);
        var advertiseOwners = new List<AdvertiseOwnerDto>();
        foreach (var owner in dto.Owners) {
            advertiseOwners.Add(new AdvertiseOwnerDto() {
                UserId = owner.UserId,
                OwnershipType = owner.OwnershipType
            });
        }
        await _repository.CreateAdvertiseAsync(createdBy, advertiseDto, advertiseOwners);
    }

    public async Task UpdateAdvertiseDetailsAsync(string userId, UpdateAdvertiseDetailsDto dto)
    {
        // Implementation
        _validator.ValidateUpdateAdvertise(dto);
        var advertiseDto = _mapper.Map<AdvertiseDto>(dto);
        await _repository.UpdateAdvertiseAsync(userId, advertiseDto);
    }

    public async Task DeleteAdvertiseAsync(string userId, string id)
    {
        await _repository.DeleteAdvertiseAsync(userId, id);
    }
    
    public async Task DeleteAdvertiseByAdminAsync(string id)
    {
        await _repository.DeleteAdvertiseByAdminAsync(id);
    }
}