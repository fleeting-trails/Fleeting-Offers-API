using System.Runtime.Serialization;

namespace FleetingOffers.Common.Enum;

public enum APP_MODULE {
    [EnumMember(Value = "AUTH")]
    AUTH,
    [EnumMember(Value = "USER")]
    USER,
    [EnumMember(Value = "ADVERTISE")]
    ADVERTISE,
    [EnumMember(Value = "CAMPAIGN")]
    CAMPAIGN,
    [EnumMember(Value = "SUBSCRIBER")]
    SUBSCRIBER,
    [EnumMember(Value = "UPLOAD")]
    UPLOAD,
    [EnumMember(Value = "LOCATION")]
    LOCATION,
    
}