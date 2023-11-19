using Shared.Enums;

namespace ContactDirectoryService.Application.Features.ContactInformations.Queries
{
    public record GetContactInformationDetailResponse
    {
        public Guid Id { get; init; }
        public ContactType ContactType { get; set; }
        public string Content { get; set; } = null!;
    }
}