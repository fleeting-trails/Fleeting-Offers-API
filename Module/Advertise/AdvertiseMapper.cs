using AutoMapper;

namespace FleetingOffers.Module.Advertise;

public class AdvertiseMapper : Profile {
    public AdvertiseMapper() {
        CreateMap<AdvertiseEntity, AdvertiseDto>().ReverseMap();
        CreateMap<CreateAdvertiseDto, AdvertiseDto>();
        CreateMap<AdvertiseOwnerEntity, AdvertiseOwnerDto>().ReverseMap();
    }
}