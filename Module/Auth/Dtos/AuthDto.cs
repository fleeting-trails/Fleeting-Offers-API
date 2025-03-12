namespace FleetingOffers.Module.Auth;

public class AuthOtpDto
{
    public string Id;
    public string UserId { get; set; }
    public string OtpValue { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;  // Defaults to current UTC time

    public DateTime? ExpireAt { get; set; }  // Optional expiration time
}

public class AuthTokenDto
{
    public string Id;
    public string UserId { get; set; }
    public string Token { get; set; }
    public string? DeviceSignature { get; set; }  // Optional
    public DateTime? Expiration { get; set; }     // Optional
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


public class PasswordDto
{
    public string Id;
    public string HashValue { get; set; }
    public string Salt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public class UserPermissionDto
{
    public string Id;
    public string SubRoleId { get; set; }
    public string PermissionString { get; set; }
}