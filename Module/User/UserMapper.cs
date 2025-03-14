using AutoMapper;

namespace FleetingOffers.Module.User;

public class UserMapper : Profile {
    public UserMapper() {
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserSubRoleEntity, UserSubRoleDto>();
        CreateMap<OrganizationProfileEntity, OrganizationProfileDto>();
        CreateMap<OrganizationProfileExtraImageEntity, OrganizationProfileExtraImageDto>();
        CreateMap<OrganizationProfilePhoneEntity, OrganizationProfilePhoneDto>();
        CreateMap<OrganizationProfileEmailEntity, OrganizationProfileEmailDto>();
        CreateMap<OrganizationSocialMediaEntity, OrganizationSocialMediaDto>();
    }
}