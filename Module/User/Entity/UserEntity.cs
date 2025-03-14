using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FleetingOffers.Common.Enums;
using FleetingOffers.Module.Auth;
using FleetingOffers.Module.File;
using FleetingOffers.Module.Location;
using Microsoft.EntityFrameworkCore;

namespace FleetingOffers.Module.User;

[Index(nameof(Username), IsUnique = true)]
public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [MaxLength(100)]
    public string? FullName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }  // Unique constraint will be applied via Fluent API

    [Required]
    public USER_ROLE Role { get; set; }  // Enum for user roles

    // Foreign Key for RestrictedUserSubRoles
    public int? RestrictedUserSubRoleId { get; set; }
    public DateTime? LastLoggedIn { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsPasswordSet { get; set; } = false;

    // Navigation properties
    public UserSubRoleEntity? UserSubRole { get; set; }

    public List<AuthTokenEntity> Tokens { get; set; } = new List<AuthTokenEntity>();
    public AuthOtpEntity? Otp { get; set; } = null;
    public PasswordEntity? Password { get; set; }
    public OrganizationProfileEntity? OrganizationProfile;
}


public class UserSubRoleEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    [MaxLength(100)]
    public string RoleName { get; set; }  // Unique constraint will be added via Fluent API

    // Navigation Property - One-to-Many relationship with UserPermissions
    public List<UserPermissionEntity> Permissions { get; set; } = new List<UserPermissionEntity>();
}

public class OrganizationProfileEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    public string UserId { get; set; }  // Foreign Key to Users

    public string? DisplayName { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }

    // Foreign Keys for CoverImage and ProfileImage
    public string? CoverImageId { get; set; }
    public FileEntity? CoverImage { get; set; }

    public string? ProfileImageId { get; set; }
    public FileEntity? ProfileImage { get; set; }

    // Extra Images as a One-to-Many Relationship
    public ICollection<OrganizationProfileExtraImageEntity> ExtraImages { get; set; } = new List<OrganizationProfileExtraImageEntity>();

    public string? Website { get; set; }
    public LocationEntity? Location { get; set; }

    // Related Collections
    public ICollection<OrganizationProfilePhoneEntity> Phones { get; set; } = new List<OrganizationProfilePhoneEntity>();
    public ICollection<OrganizationProfileEmailEntity> ContactEmails { get; set; } = new List<OrganizationProfileEmailEntity>();
    public ICollection<OrganizationSocialMediaEntity> SocialMedias { get; set; } = new List<OrganizationSocialMediaEntity>();
}

public class OrganizationProfileExtraImageEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    [ForeignKey("OrganizationProfiles")]
    public string OrganizationProfileId { get; set; }  // Foreign Key to Users
    public FileEntity Image { get; set; }
}



public class OrganizationProfilePhoneEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public string CountryCode { get; set; }  // E.g., "+1", "+91"

    [Required]
    public string Number { get; set; }

    // Foreign Key to OrganizationProfile
    [Required]
    [ForeignKey("OrganizationProfiles")]
    public string OrganizationProfileId { get; set; }
}

public class OrganizationProfileEmailEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    // Foreign Key to OrganizationProfile
    [Required]
    [ForeignKey("OrganizationProfiles")]
    public string OrganizationProfileId { get; set; }
}

public class OrganizationSocialMediaEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    public SOCIAL_MEDIA_TYPE Type { get; set; }  // Enum for social media platforms

    [Required]
    public string Link { get; set; }

    // Foreign Key to OrganizationProfile
    [Required]
    [ForeignKey("OrganizationProfiles")]
    public string OrganizationProfileId { get; set; }
}


