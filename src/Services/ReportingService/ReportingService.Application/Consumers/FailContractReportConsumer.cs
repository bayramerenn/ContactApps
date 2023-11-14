using Event.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ReportingService.Application.Common.Interfaces;
using ReportingService.Domain.Enums;

namespace ReportingService.Application.Consumers
{
    public class FailContractReportConsumer : IConsumer<FailContractReportEvent>
    {
        private readonly IApplicationDbContext _context;

        public FailContractReportConsumer(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<FailContractReportEvent> context)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == context.Message.Id);

            report!.Status = ReportStatus.Failed;

            await _context.SaveChangesAsync(default);
        }
    }
}