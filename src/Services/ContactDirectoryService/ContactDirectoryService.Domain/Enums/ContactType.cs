using System.Text.Json.Serialization;

namespace ContactDirectoryService.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContactType : byte
    {
        Phone,
        Email,
        Location
    }
}