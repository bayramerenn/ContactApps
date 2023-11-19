using ApiGateway.Models.ContactInformations;

namespace ApiGateway.Models
{
    public record ContactDetailResponse
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Company { get; init; } = null!;
        public IEnumerable<ContactInformationDetailResponse>? ContactInformations { get; init; }
    }
}