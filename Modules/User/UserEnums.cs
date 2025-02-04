using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FleetingOffers.Modules.User;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum USER_ROLE
{
    [EnumMember(Value = "SUPER_ADMIN")]
    SUPER_ADMIN,
    [EnumMember(Value = "ADMIN")]
    ADMIN,
    [EnumMember(Value = "ORGANIZATION")]
    ORGANIZATION
}
