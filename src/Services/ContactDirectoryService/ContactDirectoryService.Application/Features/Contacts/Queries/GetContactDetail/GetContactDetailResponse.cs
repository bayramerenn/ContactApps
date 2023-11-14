using ContactDirectoryService.Domain.Enums;

namespace ContactDirectoryService.Application.Features.Contacts.Queries
{
    public record GetContactDetailResponse
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Company { get; init; } = null!;
        public IEnumerable<ContactInformationResponse>? ContactInformations { get; init; }
    }

    public record ContactInformationResponse
    {
        public Guid Id { get; init; }
        public ContactType ContactType { get; set; }
        public string Content { get; set; } = null!;
    }
}