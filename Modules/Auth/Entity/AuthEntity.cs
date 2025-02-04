using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FleetingOffers.Modules.User;

namespace FleetingOffers.Modules.Auth;

public class AuthOtpEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [ForeignKey("Users")]
    public string UserId { get; set; }

    [Required]
    [MaxLength(6)]  // Assuming a 6-digit OTP
    public string OtpValue { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Defaults to current UTC time

    public DateTime? ExpireAt { get; set; }  // Optional expiration time

}

public class AuthTokenEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    [ForeignKey("Users")]
    public string UserId { get; set; }  // Foreign Key to Users/Subscribers

    [Required]
    public string Token { get; set; }

    public string? DeviceSignature { get; set; }  // Optional

    public DateTime? Expiration { get; set; }     // Optional

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


public class PasswordEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    public string HashValue { get; set; }  // Hashed password value

    public string? Salt { get; set; }  // Optional salt for additional security

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Defaults to current UTC time

    public DateTime? UpdatedAt { get; set; }  // Optional last update timestamp
}

public class UserPermissionEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    public string SubRoleId { get; set; }  // Foreign Key to UserSubRole

    [Required]
    [MaxLength(200)]
    public string PermissionString { get; set; }  // e.g., "MODULE:read:update"

    // Navigation Property
    public UserSubRoleEntity SubRole { get; set; }
}