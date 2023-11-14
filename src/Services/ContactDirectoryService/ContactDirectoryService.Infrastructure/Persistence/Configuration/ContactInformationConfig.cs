using ContactDirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactDirectoryService.Infrastructure.Persistence.Configuration
{
    public class ContactInformationConfig : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            builder
                .HasKey(ci => ci.Id);

            builder.Property(ci => ci.Content).HasMaxLength(100);

            builder.Property(ci => ci.ContactType)
                .HasConversion<string>()
                .HasMaxLength(8);

            builder
                .HasOne(ci => ci.Contact)
                .WithMany(c => c.ContactInformations)
                .HasForeignKey(ci => ci.ContactId);
        }
    }
}