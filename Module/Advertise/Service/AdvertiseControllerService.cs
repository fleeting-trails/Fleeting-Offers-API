using AutoMapper;
using FleetingOffers.Attributes;

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
    public async Task<AdvertiseDto> GetAdvertiseAsync (string userId, string id) {
        // Implementation
        var advertise = await _repository.GetOwnAdvertiseWithAllAsync(userId, id);
        if (advertise == null) {
            throw new Exception("ADVERTISE_404: Please provide a valid advertise id");
        }
        return advertise;
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
}