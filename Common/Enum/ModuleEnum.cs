using System.Runtime.Serialization;

namespace FleetingOffers.Common.Enum;

public enum APP_MODULE
{
    [EnumMember(Value = "AUTH")]
    AUTH,
    [EnumMember(Value = "USER")]
    USER,
    [EnumMember(Value = "ADVERTISE")]
    ADVERTISE,
    [EnumMember(Value = "ADVERTISE_CATEGORY")]
    ADVERTISE_CATEGORY,
    [EnumMember(Value = "ADVERTISE_INDUSTRY")]
    ADVERTISE_INDUSTRY,
    [EnumMember(Value = "PRODUCT")]
    PRODUCT,
    [EnumMember(Value = "PRODUCT_CATEGORY")]
    PRODUCT_CATEGORY,
    [EnumMember(Value = "PRODUCT_INDUSTRY")]
    PRODUCT_INDUSTRY,
    [EnumMember(Value = "PRODUCT_DEAL")]
    PRODUCT_DEAL,
    [EnumMember(Value = "CAMPAIGN")]
    CAMPAIGN,
    [EnumMember(Value = "SUBSCRIBER")]
    SUBSCRIBER,
    [EnumMember(Value = "UPLOAD")]
    UPLOAD,
    [EnumMember(Value = "LOCATION")]
    LOCATION,

}