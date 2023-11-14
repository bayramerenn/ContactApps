using Shared.BaseModels;

namespace ContactDirectoryService.Domain.Entities
{
    public class Contact : BaseEntity<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Company { get; set; } = null!;
        public ICollection<ContactInformation>? ContactInformations { get; set; }
    }
}