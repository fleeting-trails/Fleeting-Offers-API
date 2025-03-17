using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FleetingOffers.Module;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UPLOAD_STORAGE_TYPE {
    [EnumMember(Value = "LOCAL")]
    LOCAL
}