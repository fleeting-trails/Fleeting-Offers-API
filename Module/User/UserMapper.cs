using AutoMapper;

namespace FleetingOffers.Module.User;

public class UserMapper : Profile {
    public UserMapper() {
        CreateMap<UserEntity, UserDto>().ReverseMap();
        CreateMap<UserEntity, UserProjection_All>().ReverseMap();
        CreateMap<UserEntity, UserProjection_PasswordDto>().ReverseMap();
        CreateMap<UserSubRoleEntity, UserSubRoleDto>().ReverseMap();
        CreateMap<OrganizationProfileEntity, OrganizationProfileDto>().ReverseMap();
        CreateMap<OrganizationProfileExtraImageEntity, OrganizationProfileExtraImageDto>().ReverseMap();
        CreateMap<OrganizationProfilePhoneEntity, OrganizationProfilePhoneDto>().ReverseMap();
        CreateMap<OrganizationProfileEmailEntity, OrganizationProfileEmailDto>().ReverseMap();
        CreateMap<OrganizationSocialMediaEntity, OrganizationSocialMediaDto>().ReverseMap();
    }
}