using FleetingOffers.Module.User;

namespace FleetingOffers.Http;

public record HttpPayloadDto (
    string Token,
    string UserId,
    USER_ROLE Role,
    string? Device,
    bool IsValidToken
);