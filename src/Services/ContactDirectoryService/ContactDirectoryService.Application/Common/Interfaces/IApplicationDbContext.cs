using ContactDirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactDirectoryService.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Contact> Contacts { get; }
        public DbSet<ContactInformation> ContactInformations { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}