using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FleetingOffers.Modules.Auth;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AUTH_PROVIDER
{
    [EnumMember(Value = "GOOGLE")]
    GOOGLE,
    [EnumMember(Value = "FACEBOOK")]
    FACEBOOK
}

