using ContactDirectoryService.Application.Features.ContactInformations.Queries;

namespace ContactDirectoryService.Application.Features.Contacts.Queries
{
    public record GetContactDetailResponse
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Company { get; init; } = null!;
        public IEnumerable<GetContactInformationDetailResponse>? ContactInformations { get; init; }
    }
}