using AutoMapper;
using FleetingOffers.Attributes;

namespace FleetingOffers.Module.Auth;

[SingletonService]
public class AuthMapper : Profile {
    public AuthMapper() {
        CreateMap<AuthOtpDto, AuthOtpEntity>().ReverseMap();
        CreateMap<AuthTokenDto, AuthTokenEntity>().ReverseMap();
        CreateMap<PasswordDto, PasswordEntity>().ReverseMap();
        CreateMap<UserPermissionDto, UserPermissionEntity>().ReverseMap();
    }
}