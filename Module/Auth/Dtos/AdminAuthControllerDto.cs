using System.ComponentModel.DataAnnotations;
using FleetingOffers.Module.User;

namespace FleetingOffers.Module.Auth;

public record GetOtpAdminPayload (
    [Required]
    [EmailAddress]
    string Email
);
public record SetPasswordAdminPayload (
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    string Otp,
    [Required]
    string Password
);

public record LoginPayloadAdminDto (
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    string Password,
    string? Device
);
public record LoginResponseAdminDto (
    UserDto User,
    string Token
);