using AutoMapper;
using FleetingOffers.Attributes;

namespace FleetingOffers.Module.Auth;

[SingletonService]
public class AuthMapper : Profile {
    public AuthMapper() {
        CreateMap<AuthOtpDto, AuthOtpEntity>();
        CreateMap<AuthTokenDto, AuthTokenEntity>();
        CreateMap<PasswordDto, PasswordEntity>();
        CreateMap<UserPermissionDto, UserPermissionEntity>();
    }
}