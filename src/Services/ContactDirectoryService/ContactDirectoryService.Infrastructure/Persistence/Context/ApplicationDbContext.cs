using ContactDirectoryService.Application.Common.Interfaces;
using ContactDirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactDirectoryService.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts => Set<Contact>();

        public DbSet<ContactInformation> ContactInformations => Set<ContactInformation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}