using AutoMapper;

namespace FleetingOffers.Module.User;

public class UserMapper : Profile {
    public UserMapper() {
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserDto, UserEntity>();
        CreateMap<CreateUserDto, UserDto>();
        CreateMap<UpdateUserDto, UserDto>()
            .ForMember(dest => dest.FullName, opt => opt.Condition(src => !string.IsNullOrEmpty(src.FullName)))
            .ForMember(dest => dest.Username, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Username)))
            .ForMember(dest => dest.Email, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Email)))
            .ForMember(dest => dest.Role, opt => opt.Condition(src => src.Role.HasValue))
            .ForMember(dest => dest.RestrictedUserSubRoleId, opt => opt.Condition(src => src.RestrictedUserSubRoleId.HasValue));
        
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