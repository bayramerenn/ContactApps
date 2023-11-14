using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportingService.Domain.Entities;

namespace ReportingService.Infrastructure.Persistence.Configuration
{
    public class ReportConfig : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder.Property(r => r.Status)
                .HasConversion<string>()
                .HasMaxLength(10);

            builder.Property(r => r.CreatedOn)
               .ValueGeneratedOnAdd()
               .HasDefaultValueSql("NOW()");
        }
    }
}