using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FleetingOffers.Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SOCIAL_MEDIA_TYPE {
    [EnumMember(Value = "FACEBOOK")]
    FACEBOOK,
    [EnumMember(Value = "INSTAGRAM")]
    INSTAGRAM,
    [EnumMember(Value = "TWITTER")]
    TWITTER,
    [EnumMember(Value = "TIKTOK")]
    TIKTOK,
    [EnumMember(Value = "YOUTUBE")]
    YOUTUBE
} 