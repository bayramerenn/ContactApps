namespace ApiGateway.Models
{
    public record ContactListResponse
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Company { get; init; } = null!;
    }
}