using FleetingOffers.Common.Enums;
using FleetingOffers.Module.File;

namespace FleetingOffers.Module.User;

public class UserDto
{
    public string Id { get; set; }  // Auto-generated ID
    public string? FullName { get; set; }
    public string Username { get; set; }

    public string Email { get; set; }  // Unique constraint will be applied via Fluent API

    public USER_ROLE Role { get; set; }  // Enum for user roles

    // Foreign Key for RestrictedUserSubRoles
    public int? RestrictedUserSubRoleId { get; set; }

    public DateTime? LastLoggedIn { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsPasswordSet { get; set; } = false;
}


public class UserSubRoleDto
{
    public string Id { get; set; }  // Auto-generated ID
    public string RoleName { get; set; }  // Unique constraint will be added via Fluent API
}

public class OrganizationProfileDto
{
    public string Id { get; set; }  // Auto-generated ID
    public string UserId { get; set; }  // Foreign Key to Users

    public string? DisplayName { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }

    // Foreign Keys for CoverImage and ProfileImage
    public string? CoverImageId { get; set; }

    public string? ProfileImageId { get; set; }

    public string? Website { get; set; }
}

public class OrganizationProfileExtraImageDto
{
    public string Id { get; set; }  // Auto-generated ID

    public string OrganizationProfileId { get; set; }  // Foreign Key to Users
}



public class OrganizationProfilePhoneDto
{
    public string Id { get; set; }

    public string CountryCode { get; set; }  // E.g., "+1", "+91"
    public string Number { get; set; }

    // Foreign Key to OrganizationProfile
    public string OrganizationProfileId { get; set; }
}

public class OrganizationProfileEmailDto
{
    public string Id { get; set; }

    public string Email { get; set; }

    // Foreign Key to OrganizationProfile
    public string OrganizationProfileId { get; set; }
}

public class OrganizationSocialMediaDto
{
    public string Id { get; set; }

    public SOCIAL_MEDIA_TYPE Type { get; set; }  // Enum for social media platforms
    public string Link { get; set; }
    public string OrganizationProfileId { get; set; }
}

