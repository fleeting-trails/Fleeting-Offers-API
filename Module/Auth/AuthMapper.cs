using AutoMapper;
using FleetingOffers.Attributes;

namespace FleetingOffers.Module.Auth;

[SingletonService]
public class AuthMapper : Profile {
    public AuthMapper() {
        CreateMap<AuthOtpEntity, AuthOtpDto>();
        CreateMap<AuthTokenEntity, AuthTokenDto>();
        CreateMap<PasswordEntity, PasswordDto>();
        CreateMap<UserPermissionEntity, UserPermissionDto>();
    }
}