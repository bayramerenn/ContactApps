namespace ApiGateway.Models.Contacts
{
    public record ContactUpdateRequest(Guid Id,string FirstName, string LastName, string Company);
}