using AutoMapper;

namespace FleetingOffers.Module.Upload;

class UploadMapper : Profile {
    public UploadMapper() {
        CreateMap<UploadEntity, UploadDto>().ReverseMap();
    }
}