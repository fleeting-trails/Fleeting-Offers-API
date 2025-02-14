using System.ComponentModel.DataAnnotations;

namespace FleetingOffers.Modules.Auth;

public record GetOtpAdminPayload (
    [Required]
    [EmailAddress]
    string Email
);