using System.Text.Json.Serialization;

namespace ReportingService.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReportStatus : byte
    {
        InProgress,
        Completed,
        Failed
    }
}