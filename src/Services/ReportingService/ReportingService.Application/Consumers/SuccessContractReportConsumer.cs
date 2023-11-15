using Event.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ReportingService.Application.Common.Interfaces;
using ReportingService.Domain.Enums;

namespace ReportingService.Application.Consumers
{
    public class SuccessContractReportConsumer : IConsumer<SuccessContractReportEvent>
    {
        private readonly IApplicationDbContext _context;

        public SuccessContractReportConsumer(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<SuccessContractReportEvent> context)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == context.Message.Id);

            if (report != null)
            {
                report!.Status = ReportStatus.Completed;

                await _context.SaveChangesAsync(default);
            }
        }
    }
}