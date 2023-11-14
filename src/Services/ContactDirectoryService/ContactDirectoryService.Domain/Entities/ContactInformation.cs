using ContactDirectoryService.Domain.Enums;
using Shared.BaseModels;

namespace ContactDirectoryService.Domain.Entities
{
    public class ContactInformation : BaseEntity<Guid>
    {
        public ContactType ContactType { get; set; }
        public string Content { get; set; } = null!;
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; } = null!;
    }
}