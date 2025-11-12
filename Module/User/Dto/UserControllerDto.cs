using System.ComponentModel.DataAnnotations;
using FleetingOffers.Common.Enum;

namespace FleetingOffers.Module.User;

public class CreateUserDto
{
    [Required]
    [MaxLength(100)]
    public string? FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public USER_ROLE Role { get; set; }

    public int? RestrictedUserSubRoleId { get; set; }
}

public class UpdateUserDto
{
    [Required]
    public string Id { get; set; }

    [MaxLength(100)]
    public string? FullName { get; set; }

    [MaxLength(50)]
    public string? Username { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public USER_ROLE? Role { get; set; }

    public int? RestrictedUserSubRoleId { get; set; }
}