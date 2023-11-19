using System.Text.Json.Serialization;

namespace Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContactType : byte
    {
        Phone,
        Email,
        Location
    }
}