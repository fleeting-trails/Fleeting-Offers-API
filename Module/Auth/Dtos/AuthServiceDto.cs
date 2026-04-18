using FleetingOffers.Module.User;

namespace FleetingOffers.Module.Auth;

public record TokenValidationResponse (
    string? Token,
    string? UserId,
    USER_ROLE? Role,
    string? Device,
    bool IsValid,
    UserDto? User = null
);