using System.Runtime.Serialization;

namespace FleetingOffers.Common.Enum;

public enum ACCESS_CONTROL_SCOPE {
    [EnumMember(Value = "ALL")]
    ALL,
    [EnumMember(Value = "OWN")]
    OWN,
    [EnumMember(Value = "ON_CONDITION")]
    ON_CONDITION,
}
public enum ACCESS_CONTROL_TYPE {
    [EnumMember(Value = "CREATE")]
    CREATE,
    [EnumMember(Value = "READ")]
    READ,
    [EnumMember(Value = "UPDATE")]
    UPDATE,
    [EnumMember(Value = "DELETE")]
    DELETE
}