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
    public void ValidateUpdateAdvertise(UpdateAdvertiseDetailsDto dto)
    {
        if (dto.StartDate > dto.ExpirationDate)
        {
            throw new Exception("DURATION_VALIDATION: Start date must be before expiration date");
        }
    }
    
    // Advertise Category Validations

    public void ValidateCreateAdvertiseCategory(CreateAdvertiseCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("CATEGORY_VALIDATION: Category name cannot be empty");
        }
    }

    public void ValidateUpdateAdvertiseCategory(UpdateAdvertiseCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("CATEGORY_VALIDATION: Category name cannot be empty");
        }
    }

    // Advertise Industry Validations

    public void ValidateCreateAdvertiseIndustry(CreateAdvertiseIndustryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("INDUSTRY_VALIDATION: Industry name cannot be empty");
        }
    }

    public void ValidateUpdateAdvertiseIndustry(UpdateAdvertiseIndustryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new Exception("INDUSTRY_VALIDATION: Industry name cannot be empty");
        }
    }
}