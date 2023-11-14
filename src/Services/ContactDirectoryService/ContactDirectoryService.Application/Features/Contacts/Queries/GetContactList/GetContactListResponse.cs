namespace ContactDirectoryService.Application.Features.Contacts.Queries
{
    public record GetContactListResponse
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Company { get; init; } = null!;
    }
}