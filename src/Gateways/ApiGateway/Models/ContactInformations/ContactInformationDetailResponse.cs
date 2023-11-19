using Shared.Enums;

namespace ApiGateway.Models.ContactInformations
{
    public record ContactInformationDetailResponse
    {
        public Guid Id { get; init; }
        public ContactType ContactType { get; init; }
        public string Content { get; init; } = null!;
    }
}