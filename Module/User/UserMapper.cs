using AutoMapper;

namespace FleetingOffers.Module.User;

public class UserMapper : Profile {
    public UserMapper() {
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserEntity, UserProjection_All>();
        CreateMap<UserEntity, UserProjection_PasswordDto>();
        CreateMap<UserSubRoleEntity, UserSubRoleDto>();
        CreateMap<OrganizationProfileEntity, OrganizationProfileDto>();
        CreateMap<OrganizationProfileExtraImageEntity, OrganizationProfileExtraImageDto>();
        CreateMap<OrganizationProfilePhoneEntity, OrganizationProfilePhoneDto>();
        CreateMap<OrganizationProfileEmailEntity, OrganizationProfileEmailDto>();
        CreateMap<OrganizationSocialMediaEntity, OrganizationSocialMediaDto>();
    }
}