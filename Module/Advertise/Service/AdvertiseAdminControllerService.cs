using System.Threading.Tasks;
using FleetingOffers.Attributes;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseAdminControllerService {
    private readonly AdvertiseRepository _advertiseRepository;
    public AdvertiseAdminControllerService(
        AdvertiseRepository advertiseRepository
    ) {
        _advertiseRepository = advertiseRepository;
    }

    public async Task<AdvertiseDto> CreateAdvertiseAsync(List<AdvertiseOwnerPayloadDto> owners, CreateAdvertisePayloadDto payloadDto, string createdBy) {
        if (payloadDto.StartDate > payloadDto.ExpirationDate) {
            throw new Exception("VALIDATION_FAILED: Start Date cannot be greater than Expiration Date");
        }
        
        return await _advertiseRepository.CreateAdvertiseAsync(owners, payloadDto, createdBy);
    }
}