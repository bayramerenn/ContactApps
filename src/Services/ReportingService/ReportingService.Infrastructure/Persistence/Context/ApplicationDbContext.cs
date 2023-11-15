using Microsoft.EntityFrameworkCore;
using ReportingService.Application.Common.Interfaces;
using ReportingService.Domain.Entities;

namespace ReportingService.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Report> Reports => Set<Report>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}