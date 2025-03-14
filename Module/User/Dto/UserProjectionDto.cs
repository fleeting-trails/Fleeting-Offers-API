using FleetingOffers.Module.Auth;

namespace FleetingOffers.Module.User;

public class UserProjection_All : UserDto {
    public UserSubRoleEntity? UserSubRole { get; set; }
    public List<AuthTokenEntity> Tokens { get; set; } = new List<AuthTokenEntity>();
    public AuthOtpEntity? Otp { get; set; } = null;
    public PasswordEntity? Password { get; set; }
    public OrganizationProfileEntity? OrganizationProfile;
}
public class UserProjection_PasswordDto : UserDto {
    public PasswordDto Password { get; set; }
}