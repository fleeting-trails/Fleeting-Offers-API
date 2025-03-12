using System.ComponentModel.DataAnnotations;

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