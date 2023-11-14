using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.BaseModels
{
    public class ErrorDetails
    {
        public string? TraceId { get; set; }
        public string? Message { get; set; }
        public IDictionary<string, string[]>? Errors { get; set; }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
}