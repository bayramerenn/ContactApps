using Shared.Enums;

namespace ApiGateway.Models.ContactInformations
{
    public record ContactInformationCreateRequest(Guid ContactId, ContactType ContactType, string Content);
}