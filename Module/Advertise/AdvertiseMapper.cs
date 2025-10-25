using AutoMapper;

namespace FleetingOffers.Module.Advertise;

public class AdvertiseMapper : Profile {
    public AdvertiseMapper() {
        CreateMap<AdvertiseEntity, AdvertiseDto>().ReverseMap();
        CreateMap<CreateAdvertiseDto, AdvertiseDto>();
        CreateMap<AdvertiseOwnerEntity, AdvertiseOwnerDto>().ReverseMap();

        // Category mappings
        CreateMap<AdvertiseCategoryEntity, AdvertiseCategoryDto>().ReverseMap();
        CreateMap<CreateAdvertiseCategoryDto, AdvertiseCategoryDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<UpdateAdvertiseCategoryDto, AdvertiseCategoryDto>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => GenerateSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()); // Don't overwrite CreatedAt on update

        // Projections
        CreateMap<AdvertiseProjection_AllDto, AdvertiseEntity>().ReverseMap();

        // HTTP Dtos
        CreateMap<CreateAdvertiseDto, AdvertiseEntity>();
        CreateMap<UpdateAdvertiseDetailsDto, AdvertiseDto>();

    }
    
    private static string GenerateSlug(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;
            
        return name.ToLowerInvariant()
                   .Replace(" ", "-")
                   .Replace("_", "-")
                   .Trim('-');
    }
}