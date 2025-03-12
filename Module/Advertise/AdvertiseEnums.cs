using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FleetingOffers.Module.Advertise;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ADVERTISE_OWNERSHIP {
    [EnumMember(Value = "OWNER")]
    OWNER,
    [EnumMember(Value = "PARTNER")]
    PARTNER,
    [EnumMember(Value = "SPONSOR")]
    SPONSOR
}