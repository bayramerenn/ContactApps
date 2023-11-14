using Microsoft.EntityFrameworkCore;
using ReportingService.Domain.Entities;

namespace ReportingService.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Report> Reports { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}