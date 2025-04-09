using FleetingOffers.Attributes;

namespace FleetingOffers.Module.Advertise;

[ScopedService]
public class AdvertiseValidator {
    public void ValidateCreateAdvertise(CreateAdvertiseDto dto) {
        if (dto.StartDate > dto.ExpirationDate) {
            throw new Exception("DURATION_VALIDATION: Start date must be before expiration date");
        }
    }
    public void ValidateCreateAdvertiseAdmin(CreateAdvertiseAdminDto dto) {
        ValidateCreateAdvertise(dto.Advertise);
    }
    public void ValidateUpdateAdvertise(UpdateAdvertiseDetailsDto dto) {
        if (dto.StartDate > dto.ExpirationDate) {
            throw new Exception("DURATION_VALIDATION: Start date must be before expiration date");
        }
    }
}