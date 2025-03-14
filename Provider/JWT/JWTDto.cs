namespace FleetingOffers.Provider.JWT;
public class JWTPayloadDto {
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public string? Role { get; set; }
    public string? Device { get; set; }
}