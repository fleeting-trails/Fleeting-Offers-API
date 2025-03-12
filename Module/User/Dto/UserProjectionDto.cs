using FleetingOffers.Module.Auth;

namespace FleetingOffers.Module.User;

class UserProjection_PasswordDto : UserDto {
    public PasswordDto Password { get; set; }
}